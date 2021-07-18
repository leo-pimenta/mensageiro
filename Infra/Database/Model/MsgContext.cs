using Domain;
using Infra.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Database.Model
{
    public class MsgContext : DbContext
    {
        private readonly IConfiguration Configuration;
        private readonly IEncryptor Encryptor;

        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        public MsgContext(IConfiguration configuration, IEncryptor encryptor)
        {
            this.Configuration = configuration;
            this.Encryptor = encryptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
            => builder.UseNpgsql(this.Encryptor.Decrypt(
                this.Configuration["encryptor:applicationkey"],
                this.Configuration["database:connectionstring"]))
            .UseLowerCaseNamingConvention();
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("mensageiro");

            builder.Entity<User>(entity => 
            {
                entity.HasKey(user => user.Guid);
                entity.HasIndex(user => user.Email).IsUnique();
                entity.Property(user => user.Email).HasColumnName("email");
            });

            builder.Entity<UserAccount>(entity => 
            {
                entity.HasKey(account => account.UserGuid);
                entity.HasOne(account => account.User).WithOne().HasForeignKey<UserAccount>(account => account.UserGuid);
            });
        }
    }
}