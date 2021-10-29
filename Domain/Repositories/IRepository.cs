using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetEntityByIdAsync(Guid id);
        TEntity GetEntityById(Guid id);
        IQueryable<TEntity> GetAll();
    }
}