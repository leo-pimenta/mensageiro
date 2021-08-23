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

        public async Task<Contact> CreateAsync(CreateContactInvitationDto dto)
        {
            User user = await this.UserService.GetUserAsync(dto.UserGuid);
            User contactUser = await this.UserService.GetUserAsync(dto.ContactUserEmail);

            return new Contact()
            {
                User = user,
                ContactUser = contactUser   
            };
        }

        public ContactDto CreateDto(Contact contact)
        {
            User contactUser = contact.ContactUser;
            
            return new ContactDto()
            {
                Guid = contact.Guid,
                IsBlocked = contact.IsBlocked,
                Contact = new UserDto()
                {
                    Email = contactUser.Email,
                    Guid = contactUser.Guid,
                    Nickname = contactUser.Nickname
                }
            };
        }
    }
}