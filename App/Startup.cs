using System;
using System.Linq;
using App.Auth;
using App.Factories;
using App.Services;
using App.WebSockets;
using Confluent.Kafka;
using Infra.Cryptography;
using Infra.Database;
using Infra.Database.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddControllers();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App", Version = "v1" });
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
            services.AddSingleton<IResponseFactory, ResponseFactory>();
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IUserAccountFactory, UserAccountFactory>();
            services.AddSingleton<IEncryptor, Encryptor>();
            services.AddSingleton<MsgContext>(); // do not use, only for .net internal uses
            services.AddSingleton<ITokenFactory, TokenFactory>();
            InjectMessageService(services);
            
            services.AddSingleton<IUnitOfWork>(services 
                => new EntityFrameworkUnitOfWork(this.Configuration, services.GetService<IEncryptor>()));

            services.AddSingleton<IUserService>(services 
                => new UserService(
                    services.GetService<IUnitOfWork>(), 
                    new BCryptPasswordHashing(),
                    services.GetService<IUserAccountFactory>()));
        }

        private void InjectMessageService(IServiceCollection services)
        {
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = this.Configuration["kafka:server"],
                ClientId = this.Configuration["kafka:clientid"]
            };

            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = this.Configuration["kafka:server"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
                ClientId = this.Configuration["kafka:clientid"],
                
                // TODO verify how to have many instances receiving data from same topics (different group id?)
                GroupId = "mensageiro"
            };

            var producer = new ProducerBuilder<Guid, string>(producerConfig)
                .SetKeySerializer(new GuidSerializer())
                .Build();

            var consumer = new ConsumerBuilder<Guid, string>(consumerConfig)
                .SetKeyDeserializer(new GuidDeserializer())
                .Build();

            services.AddSingleton<IMessageService>(provider
                => new KafkaService(
                    consumer, 
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
            ConfigureEndpoints(app);
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
                                .AllowCredentials());
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
