using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesenvWebApi.Domain;
using DesenvWebApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesenvWebApi.Data
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public IQueryable<TEntity> GetQueryable()
            => _context.Set<TEntity>().AsQueryable();

        public Task<TEntity> GetByIdAsync(Guid id)
            => _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
    }
}