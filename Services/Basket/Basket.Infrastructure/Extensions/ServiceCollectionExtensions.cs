using Cache.Repositories;
using Cache.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //REDIS configurations
            //=============================================================================
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["configs:redisurl"];
                options.InstanceName = configuration["configs:environment"] + "01";
            });
            services.AddTransient<ICacheUnitOfWork, CacheUnitOfWork>();
            //=============================================================================
        }
    }
}
