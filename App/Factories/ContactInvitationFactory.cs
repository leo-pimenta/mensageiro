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

        public async Task<ContactInvitation> Create(CreateContactInvitationDto dto, Guid userGuid) 
        {
            User user = await this.UserService.GetUserAsync(userGuid);
            User invitedUser = await this.UserService.GetUserAsync(dto.ContactUserEmail);
            return new ContactInvitation(Guid.NewGuid(), user, invitedUser);
        }
        
        public ContactInvitationDto CreateDto(ContactInvitation invitation) =>
            new ContactInvitationDto()
            {
                Guid = invitation.Id,
                UserGuid = invitation.UserId,
                Email = invitation.User.Email,
                Nickname = invitation.User.Nickname,
                InvitedUserGuid = invitation.InvitedUserId
            };
    }
}