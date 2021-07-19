using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace App.Auth
{
    public class JwtBearerConfigurator
    {
        private readonly IConfiguration Configuration;
        private IHostEnvironment Environment;

        public JwtBearerConfigurator(IConfiguration configuration, IHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public void  ConfigureAuthentication(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        public void ConfigureOptions(JwtBearerOptions options)
        {
            bool isDevelopment = this.Environment.IsDevelopment();
            options.RequireHttpsMetadata = !isDevelopment;
            options.IncludeErrorDetails = isDevelopment;

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = this.Configuration["auth:issuer"],
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateActor = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.Configuration["auth:key"]))
            };

            var tokenValidationEvents = new TokenValidationEvents();

            options.Events = new JwtBearerEvents()
            {
                OnMessageReceived = tokenValidationEvents.OnMessageReceived
            };
        }
    }
}