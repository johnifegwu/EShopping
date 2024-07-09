
using Microsoft.EntityFrameworkCore;

namespace Discount.Infrastructure.Persistence
{
    internal class DiscountDbContext : DbContext
    {

        public DiscountDbContext(DbContextOptions<DiscountDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //ToDo: Apply table configurations here

        }
    }
}
