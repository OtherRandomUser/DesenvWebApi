using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesenvWebApi.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        IQueryable<TEntity> GetQueryable();
        Task<TEntity> GetByIdAsync(Guid id);
    }
}