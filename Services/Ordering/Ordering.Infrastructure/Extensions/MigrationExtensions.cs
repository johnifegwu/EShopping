using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using OrderingDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<OrderingDbContext>();

            if (dbContext.Database.CanConnectAsync().Result)
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
