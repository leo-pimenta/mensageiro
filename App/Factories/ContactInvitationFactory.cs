using System;
using System.Threading.Tasks;
using App.Dtos;
using App.Services;
using Domain;

namespace App.Factories
{
    public class ContactInvitationFactory : IContactInvitationFactory
    {
        private readonly IUserService UserService;

        public ContactInvitationFactory(IUserService userService)
        {
            this.UserService = userService;
        }

        public async Task<ContactInvitation> Create(CreateContactInvitationDto dto, Guid userGuid) => 
            new ContactInvitation()
            {
                User = await this.UserService.GetUserAsync(userGuid),
                InvitedUser = await this.UserService.GetUserAsync(dto.ContactUserEmail)
            };
        
        public ContactInvitationDto CreateDto(ContactInvitation invitation) =>
            new ContactInvitationDto()
            {
                Guid = invitation.Guid,
                UserGuid = invitation.UserGuid,
                Email = invitation.User.Email,
                Nickname = invitation.User.Nickname,
                InvitedUserGuid = invitation.InvitedUserGuid
            };
    }
}