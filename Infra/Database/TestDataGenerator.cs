using System.Collections.Generic;
using System.Linq;
using Domain;
using Infra.Cryptography;
using Infra.Database.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database
{
    public static class TestDataGeneratorExtension
    {
        public static EntityTypeBuilder<Contact> Insert(this EntityTypeBuilder<Contact> builder, SeedData data)
        {
            builder.HasData(data.Contacts.Select(contact => new 
            {
                contact.Id,
                contact.UserId,
                contact.ContactUserId,
                contact.GroupId
            }));

            return builder;
        }

        public static EntityTypeBuilder<UserAccount> Insert(this EntityTypeBuilder<UserAccount> builder, SeedData data)
        {
            var passwordHashing = new BCryptPasswordHashing();
            
            builder.HasData(data.Users.Select(user => new 
            {
                UserId = user.Id,
                HashedPassword = passwordHashing.Generate("123")
            }));

            return builder;
        }

        public static EntityTypeBuilder<User> Insert(this EntityTypeBuilder<User> builder, SeedData data)
        {
            builder.HasData(data.Users);
            return builder;
        }

        public static EntityTypeBuilder<ChatGroup> Insert(this EntityTypeBuilder<ChatGroup> builder, 
            SeedData data)
        {
            builder.HasData(data.Contacts.Select(contact => contact.Group).Distinct());
            return builder;
        }

        public static EntityTypeBuilder<UserGroupRelationship> Insert(
            this EntityTypeBuilder<UserGroupRelationship> builder,
            SeedData data)
        {
            builder.HasData(data.Contacts.Select(contact => new 
            {  
                GroupId = contact.GroupId,
                UserId = contact.UserId
            }));
            
            return builder;
        }
    }
}