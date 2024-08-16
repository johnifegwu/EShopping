
using Discount.Core.Entities;
using Discount.Infrastructure.Configurations;
using EFCore.UnitOfWorkCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Discount.Infrastructure.Persistence
{
    internal class DiscountDbContext : DbContext, IJayDbContext
    {

        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {
        }


        //For migrations only
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=discount.database;Database=POSTGRES_DB;Username=POSTGRES_USER;Password=POSTGRES_PASSWORD");
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<Coupon> Coupons { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CouponConfiguration).Assembly);
        }
    }
}
