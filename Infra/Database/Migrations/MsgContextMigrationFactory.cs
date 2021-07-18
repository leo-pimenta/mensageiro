using System;
using System.IO;
using Infra.Cryptography;
using Infra.Database.Model;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infra.Database.Migrations
{
    public class MsgContextMigrationFactory : IDesignTimeDbContextFactory<MsgContext>
    {
        public MsgContext CreateDbContext(string[] args)
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            IConfiguration configuration = 
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", false)
                    .AddJsonFile($"appsettings.{env}.json", true)
                    .Build();

            return new MsgContext(configuration, new Encryptor());
        }
    }
}