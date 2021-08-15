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

        public async Task<ContactInvitation> Create(ContactInvitationDto dto) => 
            new ContactInvitation()
            {
                User = await this.UserService.GetUserAsync(dto.UserGuid),
                InvitedUser = await this.UserService.GetUserAsync(dto.ContactUserEmail)
            };
    }
}