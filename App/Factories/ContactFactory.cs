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

        public async Task<Contact> CreateAsync(CreateContactInvitationDto dto, Guid userGuid)
        {
            User user = await this.UserService.GetUserAsync(userGuid);
            User contactUser = await this.UserService.GetUserAsync(dto.ContactUserEmail);
            return new Contact(Guid.NewGuid(), user, contactUser);
        }

        public IEnumerable<Contact> Create(ContactInvitation invitation) => 
            new List<Contact>()
            {
                new Contact(Guid.NewGuid(), invitation.User, invitation.InvitedUser),
                new Contact(Guid.NewGuid(), invitation.InvitedUser, invitation.User)
            };

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
                }
            };
        }
    }
}