using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Seeders;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Catalog.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RestaurantDbConnection")));
            services.AddScoped<IUnitOfWorkCore, UnitOfWorkCatalog>();
            services.AddScoped<ICatalogSeeder, CatalogSeeder>();
        }
    }
}
