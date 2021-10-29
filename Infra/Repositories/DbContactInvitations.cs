using System;
using System.Linq;
using Domain;
using Domain.Repositories;
using Infra.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class DbContactInvitations : Repository<ContactInvitation>, IContactInvitations
    {
        public DbContactInvitations(MsgContext context) : base(context) {}

        public IQueryable<ContactInvitation> GetAllByInvitedUserId(Guid guid)
        {
            IQueryable<ContactInvitation> query = from invitation in this.Context.ContactInvitations
                where invitation.Id == guid
                select invitation;
            
            return query
                .Include(invitation => invitation.InvitedUser)
                .Include(invitation => invitation.User);
        }
    }
}