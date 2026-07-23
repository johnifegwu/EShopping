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
            //var client = new MongoClient(conString);
            //var db = client.GetDatabase(dbName);
            //=============================================================================================================
            //   New retry logic and command timeouts
            //=============================================================================================================
            // Configure timeouts and retry policies at the driver level before registering EF Core
            var mongoSettings = MongoClientSettings.FromConnectionString(conString);

            // Enable automatic retries for transient network/replica set failures
            mongoSettings.RetryWrites = true;
            mongoSettings.RetryReads = true;

            // Configure driver timeouts (equivalent to CommandTimeout and connection resilience)
            mongoSettings.ConnectTimeout = TimeSpan.FromSeconds(30);
            mongoSettings.SocketTimeout = TimeSpan.FromSeconds(30);
            mongoSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(30);

            var mongoClient = new MongoClient(mongoSettings);
            var db = mongoClient.GetDatabase(dbName);

            // Register the DbContextPool using the pre-configured resilient client
            services.AddDbContextPool<CatalogDbContext>(options =>
                options.UseMongoDB(mongoClient, db.DatabaseNamespace.DatabaseName));
            //===============================================================================================================
            //services.AddDbContextPool<CatalogDbContext>(options => options.UseMongoDB(db.Client, db.DatabaseNamespace.DatabaseName));
            services.AddTransient<IJayDbContext, CatalogDbContext>();
            services.AddScoped<ICatalogSeeder, CatalogSeeder>();
            services.AddEFCoreUnitOfWork();
        }
    }
}
