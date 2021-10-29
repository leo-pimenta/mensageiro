using System;
using System.Linq;

namespace Domain.Repositories
{
    public interface IContacts : IRepository<Contact>
    {
        IQueryable<Contact> GetAllByUserId(Guid guid);
    }
}