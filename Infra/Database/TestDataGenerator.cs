using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Infra.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database
{
    public static class TestDataGeneratorExtension
    {
        public static EntityTypeBuilder<Contact> Insert(this EntityTypeBuilder<Contact> builder, IEnumerable<User> users)
        {
            IEnumerable<Contact> contacts = users.SelectMany(user => users
                .Except(new List<User>() { user })
                .Select(contactUser => new Contact(Guid.NewGuid(), user, contactUser)));
            
            builder.HasData(contacts.Select(contact => new 
            {
                contact.Id,
                contact.UserId,
                contact.ContactUserId
            }));

            return builder;
        }

        public static EntityTypeBuilder<UserAccount> Insert(this EntityTypeBuilder<UserAccount> builder, IEnumerable<User> users)
        {
            var passwordHashing = new BCryptPasswordHashing();
            
            IEnumerable<UserAccount> userAccounts = users
                .Select(user => new UserAccount(user, passwordHashing.Generate("123")));

            builder.HasData(userAccounts.Select(account => new 
            {
                account.UserId,
                account.HashedPassword
            }));

            return builder;
        }

        public static EntityTypeBuilder<User> Insert(this EntityTypeBuilder<User> builder, IEnumerable<User> users)
        {
            builder.HasData(users.Select(user => new 
            {
                user.Id,
                user.Email,
                user.Nickname
            }));

            return builder;
        }
    }
}