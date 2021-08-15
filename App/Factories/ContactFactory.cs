using System.Threading.Tasks;
using App.Dtos;
using App.Services;
using Domain;
using Infra.Database;

namespace App.Factories
{
    public class ContactFactory : IContactFactory
    {
        private readonly IUserService UserService;

        public ContactFactory(IUserService userService)
        {
            this.UserService = userService;
        }

        public async Task<Contact> CreateAsync(ContactInvitationDto dto)
        {
            User user = await this.UserService.GetUserAsync(dto.UserGuid);
            User contactUser = await this.UserService.GetUserAsync(dto.ContactUserEmail);

            return new Contact()
            {
                User = user,
                ContactUser = contactUser   
            };
        }
    }
}