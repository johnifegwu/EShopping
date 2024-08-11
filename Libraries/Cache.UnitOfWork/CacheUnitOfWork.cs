
using Cache.Repositories.AspNetCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Cache.UnitOfWork.AspNetCore
{
    internal sealed class CacheUnitOfWork : ICacheUnitOfWork
    {
        private static readonly object _createRepositoryLock = new object();

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _cacheOptions;

        public CacheUnitOfWork(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            var days = DateTime.UtcNow.AddDays(365);
            var totalseconds = days.Subtract(DateTime.UtcNow).TotalSeconds;
            _cacheOptions = new DistributedCacheEntryOptions();
            _cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(totalseconds));
            _cacheOptions.SlidingExpiration = null;
        }

        /// <summary>
        /// Override the default expiration of 365 days.
        /// </summary>
        /// <param name="ExpirationInMinutes">Expiration in minutes.</param>
        public void SetExpiration(double ExpirationInMinutes)
        {
            _cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(ExpirationInMinutes));
            _cacheOptions.SlidingExpiration = null;
        }

        /// <summary>
        /// Override the default expiration of 365 days.
        /// </summary>
        /// <param name="ExpirationInMinutes">Expiration in minutes.</param>
        /// <param name="SlidingExpirationInHours">Slinding Expiration in hours.</param>
        public void SetExpiration(double ExpirationInMinutes, double SlidingExpirationInHours)
        {
            _cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(ExpirationInMinutes));
            _cacheOptions.SetSlidingExpiration(TimeSpan.FromHours(ExpirationInMinutes));
        }

        public ICacheRepositoryCore<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (!_repositories.ContainsKey(typeof(TEntity)))
            {
                lock (_createRepositoryLock)
                {
                    if (!_repositories.ContainsKey(typeof(TEntity)))
                    {
                        CreateRepository<TEntity>();
                    }
                }
            }

            return _repositories[typeof(TEntity)] as ICacheRepositoryCore<TEntity>;
        }

        private void CreateRepository<TEntity>() where TEntity : class
        {
            _repositories.Add(typeof(TEntity), new CacheRepositoryCore<TEntity>(_distributedCache, _cacheOptions));
        }

        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _repositories?.Clear();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
