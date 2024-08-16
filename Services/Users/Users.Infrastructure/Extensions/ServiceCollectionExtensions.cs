using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Persistence;
using Users.Infrastructure.Seeders;
using EFCore.UnitOfWorkCore.Extentions;
using EFCore.UnitOfWorkCore.Interfaces;


namespace Users.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration["ConnectionStrings:UsersDbConnection"];
            services.AddDbContextPool<UsersDbContext>(options => options.UseMySQL(conString));
            services.AddTransient<IJayDbContext, UsersDbContext>();
            services.AddScoped<IUsersSeeder, UsersSeeder>();
            services.AddEFCoreUnitOfWork();
        }
    }
}
