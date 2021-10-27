using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Factories;
using Domain;
using Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMessageWriter MessageWriter;
        private readonly IContactFactory ContactFactory;

        public ContactService(IUnitOfWork unitOfWork, IMessageWriter messageWriter, IContactFactory contactFactory)
        {
            this.UnitOfWork = unitOfWork;
            this.MessageWriter = messageWriter;
            this.ContactFactory = contactFactory;
        }

        public async Task RegisterInvitationAsync(ContactInvitation invitation)
        {
            ValidateInvitationUsers(invitation);

            await this.UnitOfWork.ExecuteAsync(async context => 
            {
                await context.ContactInvitation.AddAsync(invitation);
                this.MessageWriter.InsertContactInvitation(invitation, DateTime.UtcNow);
            });
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
                IEnumerable<Contact> contacts = this.ContactFactory.Create(invitation);
                await context.Contacts.AddRangeAsync(contacts);
                context.ContactInvitation.Remove(invitation);
            });
        }

        public void RefuseInvitation(ContactInvitation invitation)
        {
            this.UnitOfWork.Execute(context => context.ContactInvitation.Remove(invitation));
        }

        public async Task<IList<Contact>> GetAllContacts(Guid guid) =>
            await this.UnitOfWork.ExecuteAsync(async context => 
                await context.Contacts
                    .Include(contact => contact.ContactUser)
                    .Include(contact => contact.Block)
                    .Where(contact => contact.UserGuid == guid)
                    .ToListAsync());
        
        public async Task<IEnumerable<ContactInvitation>> GetAllInvitationsAsync(Guid invitedUserGuid) => 
            await this.UnitOfWork.ExecuteAsync(async context => 
                await context.ContactInvitation
                    .Include(invitation => invitation.InvitedUser)
                    .Include(invitation => invitation.User)
                    .Where(invitation => invitation.InvitedUserGuid == invitedUserGuid)
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