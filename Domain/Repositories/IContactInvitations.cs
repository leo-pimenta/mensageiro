using System;
using System.Linq;

namespace Domain.Repositories
{
    public interface IContactInvitations : IRepository<ContactInvitation>
    {
        IQueryable<ContactInvitation> GetAllByInvitedUserId(Guid guid);
    }
}