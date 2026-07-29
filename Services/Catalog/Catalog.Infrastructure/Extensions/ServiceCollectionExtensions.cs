using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Seeders;
using EFCore.UnitOfWorkCore.Extentions;
using EFCore.UnitOfWorkCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


namespace Catalog.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration["ConnectionStrings:CatalogDbConnection"];
            var dbName = configuration["ConnectionStrings:DatabaseName"];

            var mongoSettings = MongoClientSettings.FromConnectionString(conString);

            // Disable replica-set specific features for standalone execution
            mongoSettings.RetryWrites = false;
            mongoSettings.RetryReads = true;
            mongoSettings.DirectConnection = true; // Bypasses replica set topology discovery

            mongoSettings.ConnectTimeout = TimeSpan.FromSeconds(30);
            mongoSettings.SocketTimeout = TimeSpan.FromSeconds(30);
            mongoSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(30);

            var mongoClient = new MongoClient(mongoSettings);

            services.AddDbContextPool<CatalogDbContext>(options =>
            {
                options.UseMongoDB(mongoClient, dbName);
            });

            services.AddTransient<IJayDbContext, CatalogDbContext>();
            services.AddScoped<ICatalogSeeder, CatalogSeeder>();
            services.AddEFCoreUnitOfWork();
        }
    }
}
