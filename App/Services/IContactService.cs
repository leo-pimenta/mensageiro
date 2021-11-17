using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace App.Services
{
    public interface IContactService
    {
        Task RegisterInvitationAsync(ContactInvitation invitation);
        Task AcceptInvitationAsync(ContactInvitation invitation);
        Task<ContactInvitation> GetInvitationAsync(Guid guid);
        Task<IEnumerable<ContactInvitation>> GetAllInvitationsAsync(Guid invitedUserGuid);
        Task<IList<Contact>> GetAllContactsAsync(Guid guid);
        Task RefuseInvitationAsync(ContactInvitation invitation);
    }
}