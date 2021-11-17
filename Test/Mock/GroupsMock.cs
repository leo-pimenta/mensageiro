using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Repositories;
using Moq;

namespace Test.Mock
{
    public class GroupsMock : RepositoryMock<ChatGroup, IChatGroups>, IObjectMock<IChatGroups>
    {
        private readonly List<UserGroupRelationship> Relationships;

        public GroupsMock() : base()
        {
            this.Relationships = new List<UserGroupRelationship>();
        }

        public override IChatGroups Create()
        {
            base.Mock
                .Setup(rep => rep.GetAllByUserId(It.IsAny<Guid>()))
                .Returns<Guid>(GetAllByUserIdMock);
            
            base.Mock
                .Setup(rep => rep.AddRelationship(It.IsAny<UserGroupRelationship>()))
                .Callback<UserGroupRelationship>(AddRelationshipMock);

            return base.Mock.Object;
        }

        private void AddRelationshipMock(UserGroupRelationship relationship)
        {
            this.Relationships.Add(relationship);
        }

        private IQueryable<ChatGroup> GetAllByUserIdMock(Guid id)
        {
            Guid groupId = this.Relationships.Single(relationship => relationship.UserId.Equals(id)).GroupId;
            return base.Entities.Where(entity => entity.Id.Equals(groupId)).AsQueryable();
        }
    }
}