using System;
using System.Linq;
using Domain;
using Domain.Repositories;
using Infra.Database.Model;

namespace Infra.Repositories
{
    public class DbMessages : Repository<Message>, IMessages
    {
        public DbMessages(MsgContext context) : base(context) {}

        public IQueryable<Message> GetPaginatedMessages(int page, int pageSize, Guid groupId)
        {
            IQueryable<Message> query = from message in base.Context.Messages
                where message.GroupId == groupId
                orderby message.Id
                select message;

            return query
                .Skip(page * pageSize)
                .Take(pageSize);
        }
    }
}