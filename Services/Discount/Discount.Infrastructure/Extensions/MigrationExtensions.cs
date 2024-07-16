
using Discount.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using DiscountDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<DiscountDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
