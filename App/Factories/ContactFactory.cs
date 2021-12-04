using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Dtos;
using App.Services;
using Domain;

namespace App.Factories
{
    public class ContactFactory : IContactFactory
    {
        private readonly IUserService UserService;

        public ContactFactory(IUserService userService)
        {
            this.UserService = userService;
        }

        public IEnumerable<Contact> Create(ContactInvitation invitation)
        {
            var group = new ChatGroup(Guid.NewGuid());

            return new List<Contact>()
            {
                new Contact(Guid.NewGuid(), invitation.User, invitation.InvitedUser, group),
                new Contact(Guid.NewGuid(), invitation.InvitedUser, invitation.User, group)
            };
        }

        public ContactDto CreateDto(Contact contact)
        {
            User contactUser = contact.ContactUser;
            
            return new ContactDto()
            {
                Guid = contact.Id,
                IsBlocked = contact.IsBlocked,
                Contact = new UserDto()
                {
                    Email = contactUser.Email,
                    Guid = contactUser.Id,
                    Nickname = contactUser.Nickname
                },
                GroupId = contact.GroupId
            };
        }
    }
}