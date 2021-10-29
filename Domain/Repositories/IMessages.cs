using System;
using System.Linq;

namespace Domain.Repositories
{
    public interface IMessages : IRepository<Message>
    {
        IQueryable<Message> GetPaginatedMessages(int page, int pageSize, Guid groupId);
    }
}