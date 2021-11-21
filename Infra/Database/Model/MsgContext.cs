using System;
using System.Collections.Generic;
using System.Linq;
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
        public DbSet<ContactInvitation> ContactInvitations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatGroup> ChatGroups { get; set; }
        public DbSet<UserGroupRelationship> UserGroupRelationships { get; set; }

        public MsgContext() {}
        
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
            var seedData = new SeedData();
            builder.HasDefaultSchema("mensageiro");
            this.BuildMessages(builder);
            this.BuildUser(builder, seedData);
            this.BuildUserAccount(builder, seedData);
            this.BuildBlock(builder);
            this.BuildContact(builder, seedData);
            this.BuildContactInvitation(builder);
            this.buildChatGroups(builder, seedData);
        }

        private void buildChatGroups(ModelBuilder builder, SeedData data)
        {
            builder.Entity<ChatGroup>(entity => 
            {
                entity.HasKey(group => group.Id);
                entity.Property(group => group.Name).IsRequired(false);
                entity.Insert(data);
            });
            
            builder.Entity<UserGroupRelationship>(entity => 
            {
                entity.HasKey(relationship => new { relationship.GroupId, relationship.UserId });
                entity.HasIndex(relationship => relationship.GroupId);
                
                entity.HasOne(relationship => relationship.Group)
                    .WithMany()
                    .HasForeignKey(relationship => relationship.GroupId)
                    .HasConstraintName("groupid")
                    .IsRequired();

                entity.HasOne(relationship => relationship.User)
                    .WithMany()
                    .HasForeignKey(relationship => relationship.UserId)
                    .HasConstraintName("userid")
                    .IsRequired();

                entity.Insert(data);
            });
        }

        private void BuildMessages(ModelBuilder builder)
        {
            builder.Entity<Message>(entity => 
            {
                entity.HasKey(message => message.Id);

                entity.HasOne(message => message.Group)
                    .WithMany()
                    .HasForeignKey(message => message.GroupId)
                    .HasConstraintName("groupid")
                    .IsRequired();
                
                entity.HasOne(message => message.User)
                    .WithMany()
                    .HasForeignKey(message => message.UserId)
                    .HasConstraintName("userid")
                    .IsRequired();
            });
        }

        private void BuildContactInvitation(ModelBuilder builder)
        {
            builder.Entity<ContactInvitation>(entity => 
            {
                entity.HasKey(invitation => invitation.Id);
                
                entity.HasOne(invitation => invitation.InvitedUser)
                    .WithMany()
                    .HasForeignKey(invitation => invitation.InvitedUserId)
                    .HasConstraintName("inviteduserid")
                    .IsRequired();

                entity.HasOne(invitation => invitation.User)
                    .WithMany()
                    .HasForeignKey(invitation => invitation.UserId)
                    .HasConstraintName("userid")
                    .IsRequired();
            });
        }

        private void BuildBlock(ModelBuilder builder)
        {
            builder.Entity<BlockInfo>(entity => 
            {
                entity.HasKey(blockInfo => blockInfo.Id);
                entity.Property(blockInfo => blockInfo.Date).IsRequired();
            });
        }

        private void BuildContact(ModelBuilder builder, SeedData data)
        {
            builder.Entity<Contact>(entity => 
            {
                entity.HasKey(contact => contact.Id);
                
                entity.HasOne(contact => contact.User)
                    .WithMany()
                    .HasForeignKey(contact => contact.UserId)
                    .HasConstraintName("userid")
                    .IsRequired();

                entity.HasOne(contact => contact.ContactUser)
                    .WithMany()
                    .HasForeignKey(contact => contact.ContactUserId)
                    .HasConstraintName("contactuserid")
                    .IsRequired();

                entity.HasOne(contact => contact.Block)
                    .WithOne()
                    .HasForeignKey<Contact>(contact => contact.BlockId)
                    .HasConstraintName("blockid")
                    .IsRequired(false);

                entity.HasOne(contact => contact.Group)
                    .WithMany()
                    .HasForeignKey(contact => contact.GroupId)
                    .HasConstraintName("groupid")
                    .IsRequired();

                entity.Insert(data);
            });
        }

        private void BuildUserAccount(ModelBuilder builder, SeedData data)
        {
            builder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(account => account.UserId);
                entity.HasOne(account => account.User).WithOne().HasForeignKey<UserAccount>(account => account.UserId).IsRequired();
                entity.Property(account => account.HashedPassword).IsRequired();
                entity.Insert(data);
            });
        }

        private void BuildUser(ModelBuilder builder, SeedData data)
        {
            builder.Entity<User>(entity => 
            {
                entity.HasKey(user => user.Id);
                entity.HasIndex(user => user.Email).IsUnique();
                entity.Property(user => user.Email).HasColumnName("email").IsRequired();
                entity.Property(user => user.Nickname).IsRequired();
                entity.Insert(data);
            });
        }
    }
}