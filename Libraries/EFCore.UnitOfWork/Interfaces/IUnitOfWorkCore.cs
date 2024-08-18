
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data.Repositories
{
    public interface IUnitOfWorkCore : IDisposable
    {
        /// <summary>
        /// Dynamic Repository.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        IRepository<T> Repository<T>() where T : class;

        /// <summary>
        /// Gets the DbContext Database Facade.
        /// </summary>
        /// <returns></returns>
        DatabaseFacade GetDatabase();

        /// <summary>
        /// Persists all changes to the database.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Persists all changes to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
