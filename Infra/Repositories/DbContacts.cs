using System;
using System.Linq;
using Domain;
using Domain.Repositories;
using Infra.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class DbContacts : Repository<Contact>, IContacts
    {
        public DbContacts(MsgContext context) : base(context) {}

        public IQueryable<Contact> GetAllByUserId(Guid id)
        {
                IQueryable<Contact> query = from contact in this.Context.Contacts
                    where contact.UserId == id
                    select contact;
            
            return query
                .Include(contact => contact.ContactUser)
                .Include(Contact => Contact.Block);
        }
    }
}