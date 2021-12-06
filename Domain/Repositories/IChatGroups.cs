using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IChatGroups : IRepository<ChatGroup>
    {
        IQueryable<ChatGroup> GetAllByUserId(Guid userId);
        Task<UserGroupRelationship> GetGroupRelationshipAsync(Guid userId, Guid groupId);
        void AddRelationship(UserGroupRelationship relationship);
        Task<ChatGroup> GetByIdAsync(Guid groupId);
    }
}