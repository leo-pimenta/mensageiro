using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Repositories;
using Moq;

namespace Test.Mock
{
    public class ContactInvitationsMock : RepositoryMock<ContactInvitation, IContactInvitations>
    {
        public override IContactInvitations Create()
        {
            base.Mock.Setup(rep => rep.GetAllByInvitedUserId(It.IsAny<Guid>()))
                .Returns<Guid>(GetAllByInvitedUserIdMock);

            return base.Create();
        }

        private IQueryable<ContactInvitation> GetAllByInvitedUserIdMock(Guid id) => 
            base.Entities.Where(entity => entity.InvitedUserId.Equals(id)).AsQueryable();
    }
}