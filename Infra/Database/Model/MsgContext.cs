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
            IEnumerable<User> users = GenerateTestUsers();
            builder.HasDefaultSchema("mensageiro");
            this.BuildMessages(builder);
            this.BuildUser(builder, users);
            this.BuildUserAccount(builder, users);
            this.BuildBlock(builder);
            this.BuildContact(builder, users);
            this.BuildContactInvitation(builder);
            this.buildChatGroups(builder, users);
        }

        private IEnumerable<User> GenerateTestUsers() => 
            new List<User>()
            {
                new User(new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), "joao.teste@teste.com", "João"),
                new User(new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), "leo.teste@teste.com", "Leo"),
                new User(new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), "mariana.teste@teste.com", "Mariana"),
                new User(new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), "matheus.teste@teste.com", "Matheus"),
                new User(new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), "claudia.teste@teste.com", "Claudia"),
                new User(new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), "luisfelipe.teste@teste.com", "Luís Felipe"),
            };

        private void buildChatGroups(ModelBuilder builder, IEnumerable<User> users)
        {
            IEnumerable<UserGroupRelationship> relationships = this.CreateRelationships(users);

            builder.Entity<ChatGroup>(entity => 
            {
                entity.HasKey(group => group.Id);
                entity.Property(group => group.Name).IsRequired(false);
                entity.Insert(relationships.Select(relationship => relationship.Group).Distinct());
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

                entity.Insert(relationships);
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

        private void BuildContact(ModelBuilder builder, IEnumerable<User> data)
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

                entity.Insert(data);
            });
        }

        private void BuildUserAccount(ModelBuilder builder, IEnumerable<User> data)
        {
            builder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(account => account.UserId);
                entity.HasOne(account => account.User).WithOne().HasForeignKey<UserAccount>(account => account.UserId).IsRequired();
                entity.Property(account => account.HashedPassword).IsRequired();
                entity.Insert(data);
            });
        }

        private void BuildUser(ModelBuilder builder, IEnumerable<User> data)
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

        private IEnumerable<UserGroupRelationship> CreateRelationships(IEnumerable<User> users)
        {
            var internalUsers = new List<User>(users);
            var relationships = new List<UserGroupRelationship>();

            foreach (var user in users)
            {
                internalUsers.Remove(user);
                
                IEnumerable<UserGroupRelationship> createdRelationships = internalUsers
                    .SelectMany(contactUsers => 
                        CreateRelationships(user, contactUsers, new ChatGroup(Guid.NewGuid())));
                
                relationships.AddRange(createdRelationships);
            }

            return relationships;
        }

        private IEnumerable<UserGroupRelationship> CreateRelationships(User user, User contactUser, ChatGroup group)
        {
            return new List<UserGroupRelationship>()
            {
                new UserGroupRelationship(user, group),
                new UserGroupRelationship(contactUser, group)
            };
        }
    }
}