
using Discount.Core.Entities;
using Discount.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Discount.Infrastructure.Persistence
{
    internal class DiscountDbContext : DbContext
    {

        public DiscountDbContext(DbContextOptions<DiscountDbContext> options): base(options)
        {
            
        }

        public DbSet<Coupon> Coupons { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CouponConfiguration());
        }
    }
}
