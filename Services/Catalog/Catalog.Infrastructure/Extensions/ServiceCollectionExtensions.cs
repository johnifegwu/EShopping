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
            var client = new MongoClient(conString);
            var db = client.GetDatabase(dbName);
            services.AddDbContextPool<CatalogDbContext>(options => options.UseMongoDB(db.Client, db.DatabaseNamespace.DatabaseName));
            services.AddTransient<IJayDbContext, CatalogDbContext>();
            services.AddScoped<ICatalogSeeder, CatalogSeeder>();
            services.AddEFCoreUnitOfWork();
        }
    }
}
