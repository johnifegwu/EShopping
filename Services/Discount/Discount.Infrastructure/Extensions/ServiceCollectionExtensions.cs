using Discount.Infrastructure.Persistence;
using EFCore.UnitOfWorkCore.Extentions;
using Discount.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EFCore.UnitOfWorkCore.Interfaces;

namespace Discount.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration["ConnectionStrings:DiscountDbConnection"];
            services.AddDbContextPool<DiscountDbContext>(options => options.UseNpgsql(conString));
            services.AddTransient<IJayDbContext, DiscountDbContext>();
            services.AddScoped<IDiscountSeeder, DiscountSeeder>();
            services.AddEFCoreUnitOfWork();
        }
    }
}
