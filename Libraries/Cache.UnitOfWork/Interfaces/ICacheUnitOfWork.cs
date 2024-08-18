
namespace Cache.Repositories.AspNetCore
{
    /// <summary>
    /// ICache Unit of Work.
    /// </summary>
    public interface ICacheUnitOfWork : IDisposable
    {

        /// <summary>
        /// Repository.
        /// </summary>
        /// <typeparam name="TEntity">Entity class.</typeparam>
        /// <returns></returns>
        ICacheRepositoryCore<TEntity> Repository<TEntity>() where TEntity : class;

        /// <summary>
        /// Override the default expiration of 365 days.
        /// </summary>
        /// <param name="ExpirationInMinutes">Expiration in minutes.</param>
        void SetExpiration(double ExpirationInMinutes);

        /// <summary>
        /// Override the default expiration of 365 days.
        /// </summary>
        /// <param name="ExpirationInMinutes">Expiration in minutes.</param>
        /// <param name="SlidingExpirationInHours">Slinding Expiration in hours.</param>
        void SetExpiration(double ExpirationInMinutes, double SlidingExpirationInHours);

    }
}
