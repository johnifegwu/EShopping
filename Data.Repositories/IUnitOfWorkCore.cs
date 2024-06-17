using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IUnitOfWorkCore : IDisposable
    {
        DbContext GetContext();
        IRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
