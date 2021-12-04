using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using Infra.Database.Model;

namespace Infra.Repositories
{
    public class DbChatGroups : Repository<ChatGroup>, IChatGroups
    {
        public DbChatGroups(MsgContext context) : base(context) {}

        public void AddRelationship(UserGroupRelationship relationship)
        {
            this.Context.UserGroupRelationships.Add(relationship);
        }

        public IQueryable<ChatGroup> GetAllByUserId(Guid userId) => 
            from relationship in base.Context.UserGroupRelationships
            join chatGroup in base.Context.Set<ChatGroup>() on relationship.GroupId equals chatGroup.Id
            where relationship.UserId == userId
            select chatGroup;

        public async Task<UserGroupRelationship> GetGroupRelationshipAsync(Guid userId, Guid groupId) => 
            await this.Context.UserGroupRelationships.FindAsync(groupId, userId);
    }
}