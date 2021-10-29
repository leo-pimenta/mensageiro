using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Infra.Database.Model;

namespace Infra.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MsgContext Context;

        protected Repository(MsgContext context) => this.Context = context;

        public virtual void Add(TEntity entity) => this.Context.Add(entity);
        public virtual void AddRange(IEnumerable<TEntity> entities) => this.Context.AddRange(entities);
        public virtual void Delete(TEntity entity) => this.Context.Remove(entity);
        public virtual IQueryable<TEntity> GetAll() => from entity in this.Context.Set<TEntity>() select entity;
        public TEntity GetEntityById(Guid id) => this.Context.Find<TEntity>(id);
        public virtual async Task<TEntity> GetEntityByIdAsync(Guid id) => await this.Context.FindAsync<TEntity>(id);
        public virtual void Update(TEntity entity) => this.Context.Update(entity);
    }
}