using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork UnitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public async Task RegisterInvitationAsync(ContactInvitation invitation)
        {
            ValidateInvitationUsers(invitation);

            await this.UnitOfWork.ExecuteAsync(async context => 
                await context.ContactInvitation.AddAsync(invitation));
        }

        public async Task<ContactInvitation> GetInvitationAsync(Guid guid) => 
            await this.UnitOfWork.ExecuteAsync(async context => 
            {
                ContactInvitation invitation = await context.ContactInvitation.FindAsync(guid);
                ValidateInvitationFound(invitation);
                return invitation;
            });

        public async Task AcceptInvitation(ContactInvitation invitation)
        {
            await this.UnitOfWork.ExecuteAsync(async context => 
            {
                var contacts = new List<Contact>()
                {
                    new Contact() { UserGuid = invitation.UserGuid, ContactUserGuid = invitation.InvitedUserGuid },
                    new Contact() { UserGuid = invitation.InvitedUserGuid, ContactUserGuid = invitation.UserGuid }
                };

                await context.Contacts.AddRangeAsync(contacts);
                var a = context.ContactInvitation.Remove(invitation);
            });
        }

        public async Task<IList<Contact>> GetAllContacts(Guid guid) =>
            await this.UnitOfWork.ExecuteAsync(async context => 
                await context.Contacts
                    .Include(contact => contact.ContactUser)
                    .Include(contact => contact.Block)
                    .Where(contact => contact.UserGuid == guid)
                    .ToListAsync());

        private void ValidateInvitationFound(ContactInvitation invitation)
        {
            if (invitation == null)
            {
                throw new ContactInvitationNotFoundException("The invitation does not exist.");
            }
        }

        private void ValidateInvitationUsers(ContactInvitation invitation)
        {
            if (invitation.InvitedUser == null)
            {
                throw new UserNotFoundException("Invited user does not exist.");
            }
            else if (invitation.User == null)
            {
                throw new UserNotFoundException("User does not exist.");
            }    
        }       
    }
}