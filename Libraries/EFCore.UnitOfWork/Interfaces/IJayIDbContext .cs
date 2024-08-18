
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.UnitOfWorkCore.Interfaces
{
    /// <summary>
    /// IJay DbContext.
    /// </summary>
    public interface IJayDbContext : IDisposable
    {
        /// <summary>
        /// Sets the provided entity to the DbContext.
        /// </summary>
        /// <typeparam name="TEntity">Entity class.</typeparam>
        /// <returns></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets the DbContext Database Facade.
        /// </summary>
        /// <returns></returns>
        DatabaseFacade Database { get; }

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
