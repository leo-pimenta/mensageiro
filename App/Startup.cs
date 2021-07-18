using System.Linq;
using App.Factories;
using App.Services;
using Infra.Cryptography;
using Infra.Database;
using Infra.Database.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            this.Configuration = configuration;
            
            if (env.IsDevelopment())
            {
                MigrateUp();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            InjectDependencies(services);
            ConfigureValidationJsonResponse(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App", Version = "v1" });
            });
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
            
            services.AddSingleton<IUnitOfWork>(services 
                => new EntityFrameworkUnitOfWork(this.Configuration, services.GetService<IEncryptor>()));

            services.AddSingleton<IUserService>(services 
                => new UserService(
                    services.GetService<IUnitOfWork>(), 
                    new BCryptPasswordHashing(),
                    services.GetService<IUserAccountFactory>()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
