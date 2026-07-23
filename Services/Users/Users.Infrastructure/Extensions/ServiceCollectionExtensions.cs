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
            services.AddDbContextPool<UsersDbContext>(options => options.UseMySQL(conString, mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                mySqlOptions.CommandTimeout(30);
            }));
            services.AddTransient<IJayDbContext, UsersDbContext>();
            services.AddScoped<IUsersSeeder, UsersSeeder>();
            services.AddEFCoreUnitOfWork();
        }
    }
}
