

using Data.Repositories;

namespace Catalog.Infrastructure.Seeders
{
    internal class CatalogSeeder(IUnitOfWorkCore context) : ICatalogSeeder
    {

        public async Task Seed()
        {
            if (await context.GetContext().Database.CanConnectAsync())
            {
                //ToDo: Initialize Catalog database here after checking if no record exist.
            }
        }
    }
}
