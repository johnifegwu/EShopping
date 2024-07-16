
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration["ConnectionStrings:OrderingDbConnection"];
            services.AddDbContextPool<OrderingDbContext>(options => options.UseSqlServer(conString));
            services.AddTransient<IUnitOfWorkCore, UnitOfWorkOrdering>();
        }
    }
}
