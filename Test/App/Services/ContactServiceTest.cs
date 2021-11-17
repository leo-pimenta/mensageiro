using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Factories;
using App.Services;
using Domain;
using Domain.Repositories;
using Infra.Database.Model;
using Test.Mock;
using Xunit;

namespace Test.App.Services
{
    public class ContactServiceTest
    {
        private readonly IChatGroups Groups;
        private readonly ContactService ContactService;
        private readonly IUserService UserService;
        private readonly IContacts Contacts;
        private readonly IContactInvitations ContactInvitations;

        public ContactServiceTest()
        {
            MsgContext context = new MsgContextMock().Create();
            this.Contacts = new ContactsMock().Create();
            this.ContactInvitations = new ContactInvitationsMock().Create();
            IMessageWriter messageWriter = new MessageWriterMock().Create();
            this.UserService = new UserServiceMock().Create();
            var contactFactory = new ContactFactory(this.UserService);
            this.Groups = new GroupsMock().Create(); 

            this.ContactService = new ContactService(context, this.Contacts, this.ContactInvitations, 
                this.Groups, messageWriter, contactFactory);
        }

        [Fact]
        public async Task ShouldDeleteInvitationWhenAccepted()
        {
            ContactInvitation invitation = await SetupInvitationTestAsync();
            await this.ContactService.AcceptInvitationAsync(invitation);
            
            IEnumerable<ContactInvitation> invitations = this.ContactInvitations
                .GetAllByInvitedUserId(invitation.InvitedUserId).ToList();

            Assert.Empty(invitations);
        }

        [Fact]
        public async Task ShouldCreateUserGroupWhenInvitationIsAccepted()
        {
            ContactInvitation invitation = await SetupInvitationTestAsync();
            await this.ContactService.AcceptInvitationAsync(invitation);
            IEnumerable<ChatGroup> userGroups = this.Groups.GetAllByUserId(invitation.UserId);
            Assert.Single(userGroups);
        }

        [Fact]
        public async Task ShouldCreateInvitedUserGroupWhenInvitationIsAccepted()
        {
            ContactInvitation invitation = await SetupInvitationTestAsync();
            await this.ContactService.AcceptInvitationAsync(invitation);
            IEnumerable<ChatGroup> invitedUserGroups = this.Groups.GetAllByUserId(invitation.InvitedUserId);
            Assert.Single(invitedUserGroups);
        }

        [Fact]
        public async Task ShouldCreateUserContactWhenInvitationIsAccepted()
        {
            ContactInvitation invitation = await SetupInvitationTestAsync();
            await this.ContactService.AcceptInvitationAsync(invitation);   
            IEnumerable<Contact> userContacts = this.Contacts.GetAllByUserId(invitation.UserId);
            Assert.Single(userContacts);
        }

        [Fact]
        public async Task ShouldCreateInvitedUserContactWhenInvitationIsAccepted()
        {
            ContactInvitation invitation = await SetupInvitationTestAsync();
            await this.ContactService.AcceptInvitationAsync(invitation);
            IEnumerable<Contact> invitedUserContacts = this.Contacts.GetAllByUserId(invitation.InvitedUserId);
            Assert.Single(invitedUserContacts);
        }

        private async Task<ContactInvitation> SetupInvitationTestAsync()
        {
            var user = new User(Guid.NewGuid(), "email@email.com", "test-user");
            var invitedUser = new User(Guid.NewGuid(), "email2@email.com", "test-user2");
            var invitation = new ContactInvitation(Guid.NewGuid(), user, invitedUser);

            await this.UserService.RegisterUserAsync(user, "123");
            await this.UserService.RegisterUserAsync(invitedUser, "123");
            this.ContactInvitations.Add(invitation);
            return invitation;
        }
    }
}