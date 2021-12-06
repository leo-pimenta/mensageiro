using System;
using System.Linq;
using Domain;
using Domain.Repositories;
using Infra.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class DbMessages : Repository<Message>, IMessages
    {
        public DbMessages(MsgContext context) : base(context) {}

        public IQueryable<Message> GetPaginatedMessages(int page, int pageSize, Guid groupId)
        {
            IQueryable<Message> query = from message in base.Context.Messages
                where message.GroupId == groupId
                orderby message.SentAt
                select message;

            return query
                .Include(message => message.User)
                .Skip((page-1) * pageSize)
                .Take(pageSize);
        }
    }
}