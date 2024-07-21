using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Persistence;

namespace Users.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using UsersDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<UsersDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
