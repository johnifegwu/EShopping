using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence
{
    internal class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

    }
}
