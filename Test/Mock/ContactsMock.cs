using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Repositories;
using Infra.Repositories;
using Moq;

namespace Test.Mock
{
    public class ContactsMock : RepositoryMock<Contact, IContacts>, IObjectMock<IContacts>
    {
        public override IContacts Create()
        {
            base.Mock
                .Setup(rep => rep.GetAllByUserId(It.IsAny<Guid>()))
                .Returns<Guid>(GetAllByUserIdMock);
            
            return base.Mock.Object;
        }

        private IQueryable<Contact> GetAllByUserIdMock(Guid id) => base.Entities.Where(entity => entity.UserId.Equals(id)).AsQueryable();
    }
}