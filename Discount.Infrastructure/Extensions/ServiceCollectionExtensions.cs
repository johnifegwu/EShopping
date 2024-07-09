
using Data.Repositories;
using Discount.Infrastructure.Persistence;
using Discount.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration["ConnectionStrings:DiscountDbConnection"];
            services.AddDbContext<DiscountDbContext>(options => options.UseNpgsql(conString), ServiceLifetime.Transient);
            services.AddTransient<IUnitOfWorkCore, UnitOfWorkDiscount>();
        }
    }
}
