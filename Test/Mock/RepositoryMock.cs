using System;
using System.Collections.Generic;
using Domain.Repositories;
using Moq;

namespace Test.Mock
{
    public abstract class RepositoryMock<TEntity, TRepository> : IObjectMock<TRepository>
         where TRepository : class, IRepository<TEntity>
    {
        protected readonly Mock<TRepository> Mock = new Mock<TRepository>();

        protected List<TEntity> Entities = new List<TEntity>();

        public RepositoryMock() => this.MockMethods();
        public virtual TRepository Create() => this.Mock.Object;

        private void MockMethods()
        {
            this.Mock
                .Setup(rep => rep.Add(It.IsAny<TEntity>()))
                .Callback<TEntity>(this.AddMock);
            
            this.Mock
                .Setup(rep => rep.AddRange(It.IsAny<IEnumerable<TEntity>>()))
                .Callback<IEnumerable<TEntity>>(this.AddRangeMock);
            
            this.Mock
                .Setup(rep => rep.Delete(It.IsAny<TEntity>()))
                .Callback<TEntity>(this.DeleteMock);
        }

        private void AddMock(TEntity entity)
        {
            this.Entities.Add(entity);
        }

        private void AddRangeMock(IEnumerable<TEntity> entities)
        {
            this.Entities.AddRange(entities);
        }

        private void DeleteMock(TEntity entity)
        {
            this.Entities.Remove(entity);
        }
    }
}