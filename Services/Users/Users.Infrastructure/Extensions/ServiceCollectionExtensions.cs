
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Persistence;
using Users.Infrastructure.Repositories;
using Users.Infrastructure.Seeders;


namespace Users.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration["ConnectionStrings:UsersDbConnection"];
            services.AddDbContextPool<UsersDbContext>(options => options.UseMySQL(conString));
            services.AddTransient<IUnitOfWorkCore, UnitOfWorkUsers>();
            services.AddScoped<IUsersSeeder, UsersSeeder>();
        }
    }
}
