
using EFCore.UnitOfWorkCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Infrastructure.Configurations;

namespace Ordering.Infrastructure.Persistence
{
    internal class OrderingDbContext : DbContext, IJayDbContext
    {
        public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; init; }
        public DbSet<OrderDetail> OrderDetails { get; init; }

        //For migrations only
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=localhost,1433;TrustServerCertificate=true;Initial Catalog=eShopping;User Id=sa;Password=Admin@12345678;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        }
    }
}
