
using Cache.Repositories.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cache.UnitOfWork.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCacheUnitOfWork(this IServiceCollection services)
        {
            //Add ICacheUnitOfWork
            services.AddTransient<ICacheUnitOfWork, CacheUnitOfWork>();
        }
    }
}
