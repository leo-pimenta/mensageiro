using System;
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
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<BlockInfo> Blocks { get; set; }
        public DbSet<ContactInvitation> ContactInvitation { get; set; }

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
            this.BuildUser(builder);
            this.BuildUserAccount(builder);
            this.BuildBlock(builder);
            this.BuildContact(builder);
            this.BuildContactInvitation(builder);
        }

        private void BuildContactInvitation(ModelBuilder builder)
        {
            builder.Entity<ContactInvitation>(entity => 
            {
                entity.HasKey(invitation => invitation.Guid);
                
                entity.HasOne(invitation => invitation.InvitedUser)
                    .WithMany()
                    .HasForeignKey(invitation => invitation.InvitedUser.Guid)
                    .HasConstraintName("inviteduserguid")
                    .IsRequired();

                entity.HasOne(invitation => invitation.User)
                    .WithMany()
                    .HasForeignKey(invitation => invitation.User.Guid)
                    .HasConstraintName("userguid")
                    .IsRequired();
            });
        }

        private void BuildBlock(ModelBuilder builder)
        {
            builder.Entity<BlockInfo>(entity => 
            {
                entity.HasKey(blockInfo => blockInfo.Guid);
                entity.Property(blockInfo => blockInfo.Date).IsRequired();
            });
        }

        private void BuildContact(ModelBuilder builder)
        {
            builder.Entity<Contact>(entity => 
            {
                entity.HasKey(contact => contact.Guid);
                
                entity.HasOne(contact => contact.User)
                    .WithOne()
                    .HasForeignKey<Contact>("userguid")
                    .IsRequired();

                entity.HasOne(contact => contact.ContactUser)
                    .WithOne()
                    .HasForeignKey<Contact>("contactuserguid")
                    .IsRequired();

                entity.HasOne(contact => contact.Block)
                    .WithOne()
                    .HasForeignKey<Contact>("blockguid")
                    .IsRequired(false);
            });
        }

        private void BuildUserAccount(ModelBuilder builder)
        {
            builder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(account => account.UserGuid);
                entity.HasOne(account => account.User).WithOne().HasForeignKey<UserAccount>(account => account.UserGuid).IsRequired();
                entity.Property(account => account.HashedPassword).IsRequired();
            });
        }

        private void BuildUser(ModelBuilder builder)
        {
            builder.Entity<User>(entity => 
            {
                entity.HasKey(user => user.Guid);
                entity.HasIndex(user => user.Email).IsUnique();
                entity.Property(user => user.Email).HasColumnName("email").IsRequired();
                entity.Property(user => user.Nickname).IsRequired();
            });
        }
    }
}