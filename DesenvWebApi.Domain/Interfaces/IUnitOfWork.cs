using System.Threading.Tasks;

namespace DesenvWebApi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IRepository<T> GetRepository<T>() where T : Entity;
    }
}