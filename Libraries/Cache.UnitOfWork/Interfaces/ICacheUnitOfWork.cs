
namespace Cache.Repositories.AspNetCore
{
    public interface ICacheUnitOfWork : IDisposable
    {
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
