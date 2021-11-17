using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Factories;
using Domain;
using Domain.Repositories;
using Infra.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class ContactService : DbService, IContactService
    {
        private readonly IContacts Contacts;
        private readonly IContactInvitations ContactInvitations;
        private readonly IChatGroups Groups;
        private readonly IMessageWriter MessageWriter;
        private readonly IContactFactory ContactFactory;

        public ContactService(
            MsgContext context,
            IContacts contacts, 
            IContactInvitations contactInvitations,
            IChatGroups groups,
            IMessageWriter messageWriter, 
            IContactFactory contactFactory) : base(context)
        {
            this.Contacts = contacts;
            this.ContactInvitations = contactInvitations;
            this.Groups = groups;
            this.MessageWriter = messageWriter;
            this.ContactFactory = contactFactory;
        }

        public async Task RegisterInvitationAsync(ContactInvitation invitation)
        {
            ValidateInvitationUsers(invitation);
            this.ContactInvitations.Add(invitation);
            await base.SaveDbChangesAsync();
            this.MessageWriter.InsertContactInvitation(invitation, DateTime.UtcNow);
        }

        public async Task<ContactInvitation> GetInvitationAsync(Guid guid)
        {
            ContactInvitation invitation = await this.ContactInvitations.GetEntityByIdAsync(guid);
            ValidateInvitationFound(invitation);
            return invitation;
        }

        public async Task AcceptInvitationAsync(ContactInvitation invitation)
        {
            IEnumerable<Contact> contacts = this.ContactFactory.Create(invitation);
            this.Contacts.AddRange(contacts);
            this.ContactInvitations.Delete(invitation);
            
            // TODO refactor
            var group = new ChatGroup(Guid.NewGuid());
            var userRelationship = new UserGroupRelationship(invitation.User, group);
            var contactUserRelationship = new UserGroupRelationship(invitation.InvitedUser, group);

            this.Groups.Add(group);
            this.Groups.AddRelationship(userRelationship);
            this.Groups.AddRelationship(contactUserRelationship);

            await this.SaveDbChangesAsync();
        }

        public async Task RefuseInvitationAsync(ContactInvitation invitation)
        {
            this.ContactInvitations.Delete(invitation);
            await this.SaveDbChangesAsync();
        }

        public async Task<IList<Contact>> GetAllContactsAsync(Guid guid) => await this.Contacts.GetAllByUserId(guid).ToListAsync();
        
        public async Task<IEnumerable<ContactInvitation>> GetAllInvitationsAsync(Guid invitedUserGuid) => 
            await this.ContactInvitations.GetAllByInvitedUserId(invitedUserGuid).ToListAsync();

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