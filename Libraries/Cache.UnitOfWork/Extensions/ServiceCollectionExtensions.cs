
using Cache.Repositories.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cache.UnitOfWork.AspNetCore.Extensions
{
    /// <summary>
    /// ServiceCollection Extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds CacheUnitOfWork to the Service collection.
        /// </summary>
        /// <param name="services"></param>
        public static void AddCacheUnitOfWork(this IServiceCollection services)
        {
            //Add ICacheUnitOfWork
            services.AddTransient<ICacheUnitOfWork, CacheUnitOfWork>();
        }
    }
}
