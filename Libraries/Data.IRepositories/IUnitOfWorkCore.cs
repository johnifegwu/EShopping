
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data.Repositories
{
    public interface IUnitOfWorkCore : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        DatabaseFacade GetDatabase();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
