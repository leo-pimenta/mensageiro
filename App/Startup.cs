using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using App.Auth;
using App.Controllers;
using App.Factories;
using App.Services;
using App.WebSockets;
using Confluent.Kafka;
using Domain.Repositories;
using Infra.Cryptography;
using Infra.Database;
using Infra.Database.Model;
using Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

namespace App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly IHostEnvironment Environment;

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            this.Configuration = configuration;
            this.Environment = env;
            
            if (this.Environment.IsDevelopment())
            {
                MigrateUp();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            InjectDependencies(services);
            ConfigureValidationJsonResponse(services);
            services.AddCors();
            ConfigureAuthentication(services);
            ConfigureAuthorization(services);
            services.AddControllers();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App", Version = "v1" });
            });
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options => 
            {
                options.AddPolicy("accessPolicy", policy => 
                {
                    policy.RequireClaim("canAccess", "true");
                });

                options.AddPolicy("refreshPolicy", policy => 
                {
                    policy.RequireClaim("canRefresh", "true");
                });

                options.DefaultPolicy = options.GetPolicy("accessPolicy");
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUserIdentity, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<MsgContext>()
                .AddDefaultTokenProviders();

            IdentityModelEventSource.ShowPII = this.Environment.IsDevelopment();
            var jwtConfigurator = new JwtBearerConfigurator(this.Configuration, this.Environment);

            services
                .AddAuthentication(jwtConfigurator.ConfigureAuthentication)
                .AddJwtBearer(jwtConfigurator.ConfigureOptions);
        }

        private void ConfigureValidationJsonResponse(IServiceCollection services)
        {
            services.PostConfigure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = 
                context => 
            {
                return new BadRequestObjectResult(new 
                {
                    Message = "Invalid request data. See \"Errors\" array.",
                    Errors = context.ModelState.Values
                        .SelectMany(value => value.Errors)
                        .Select(error => error.ErrorMessage)
                });
            });
        }

        private void InjectDependencies(IServiceCollection services)
        {
            services.AddSingleton<IEncryptor, Encryptor>();
            services.AddSingleton<IPasswordHashing, BCryptPasswordHashing>();
            services.AddSingleton<IResponseFactory, ResponseFactory>();
            InjectDatabaseDependencies(services);
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IUserAccountFactory, UserAccountFactory>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactFactory, ContactFactory>();
            services.AddScoped<IContactInvitationFactory, ContactInvitationFactory>();
            services.AddSingleton<ITokenFactory, TokenFactory>();
            services.AddSingleton<ISendCommand, SignalRSendCommand>();
            
            InjectMessageReader(services);
            InjectMessageWriter(services);
            
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IMessageService, MessageService>();
        }

        private void InjectDatabaseDependencies(IServiceCollection services)
        {
            services.AddScoped<MsgContext>();
            services.AddScoped<IChatGroups, DbChatGroups>();
            services.AddScoped<IContactInvitations, DbContactInvitations>();
            services.AddScoped<IContacts, DbContacts>();
            services.AddScoped<IMessages, DbMessages>();
            services.AddScoped<IUsers, DbUsers>();
            services.AddScoped<IUserAccounts, DbUserAccount>();
        }

        private void InjectMessageReader(IServiceCollection services)
        {
            var consumer = new ConsumerBuilder<Guid, string>(new ConsumerConfig()
                {
                    BootstrapServers = this.Configuration["kafka:server"],
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    ClientId = this.Configuration["kafka:clientid"],
                    
                    // TODO verify how to have many instances receiving data from same topics (different group id?)
                    GroupId = "mensageiro"
                })
                .SetKeyDeserializer(new GuidDeserializer())
                .Build();
            
            services.AddScoped<IMessageReader>(provider => 
            {
                var kafkaReader = new KafkaReader(
                    consumer, 
                    provider.GetService<IUserService>(),
                    provider.GetService<ISendCommand>(),
                    provider.GetService<IConfiguration>());

                kafkaReader.Start();
                return kafkaReader;
            });
        }

        private void InjectMessageWriter(IServiceCollection services)
        {
            var producer = new ProducerBuilder<Guid, string>(new ProducerConfig()
                {
                    BootstrapServers = this.Configuration["kafka:server"],
                    ClientId = this.Configuration["kafka:clientid"]
                })
                .SetKeySerializer(new GuidSerializer())
                .Build();

            IAdminClient admin = new AdminClientBuilder(new AdminClientConfig()
                {
                    ClientId = this.Configuration["kafka:clientid"],
                    BootstrapServers = this.Configuration["kafka:server"]
                })
                .Build();
            
            services.AddScoped<IMessageWriter>(provider => 
                new KafkaWriter(
                    admin,
                    producer, 
                    provider.GetService<IConfiguration>(), 
                    provider.GetService<IUserService>()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                ConfigureSwagger(app);
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            ConfigureCors(app);
            app.UseAuthentication();
            app.UseAuthorization();
            ConfigureRequestGlobalErrorHandler(app);
            ConfigureEndpoints(app);
        }

        private void ConfigureRequestGlobalErrorHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder => 
            {
                builder.Run(async context => 
                {
                    Exception e = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

                    switch (e)
                    {
                        case BadRequestException ex:
                            await SetErrorResponseAsync(ex.Message, HttpStatusCode.BadRequest, context);
                            break;
                        case ForbiddenExcepion ex:
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            break;
                        default:
                            const string Message = "Internal server error.";
                            await SetErrorResponseAsync(Message, HttpStatusCode.InternalServerError, context);
                            break;
                    }
                });
            });
        }

        private async Task SetErrorResponseAsync(string message, HttpStatusCode statusCode, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ResponseFactory().Create(message)));
        }

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App v1"));
        }

        private void ConfigureEndpoints(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }

        private void ConfigureCors(IApplicationBuilder app)
        {
            app.UseCors(options =>
                options.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials()
                    .WithOrigins(this.Configuration.GetArray("cors:origins")));
        }

        private void MigrateUp()
        {
            using (var context = new MsgContext(this.Configuration, new Encryptor()))
            {
                context.Database.Migrate();
            }
        }
    }
}
