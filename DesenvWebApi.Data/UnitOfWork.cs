using System;
using System.Threading.Tasks;
using DesenvWebApi.Domain;
using DesenvWebApi.Domain.Interfaces;

namespace DesenvWebApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IServiceProvider _serviceProvider;
        private ApplicationDbContext _context;

        public UnitOfWork(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : Entity
            => _serviceProvider.GetService(typeof(IRepository<T>)) as IRepository<T>;

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
}