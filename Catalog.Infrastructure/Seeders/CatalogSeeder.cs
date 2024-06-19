

using Catalog.Core.Entities;
using Data.Repositories;
using MongoDB.Bson;

namespace Catalog.Infrastructure.Seeders
{
    internal class CatalogSeeder(IUnitOfWorkCore context) : ICatalogSeeder
    {

        public async Task Seed()
        {
            try
            {
                //Initialize Catalog database here after checking if no record exist.
                if (context.Repository<ProductType>().Read().First() == null)
                {
                    //Create new ProductTypes
                    await context.Repository<ProductType>().AddRangeAsync(GetProductTypes());
                }
                if (context.Repository<ProductBrand>().Read().First() == null)
                {
                    //Create new ProductBrands
                    await context.Repository<ProductBrand>().AddRangeAsync(GetProductBrands());
                }
            }
            catch (InvalidOperationException ex)
            {
                //Create new ProductTypes
                await context.Repository<ProductType>().AddRangeAsync(GetProductTypes());
                //Create new ProductBrands
                await context.Repository<ProductBrand>().AddRangeAsync(GetProductBrands());
            }

            //For SQL Server
            //==================================================================================
            //var canconnect = await context.GetContext().Database.CanConnectAsync();
            //if (canconnect)
            //{
            //    //Initialize Catalog database here after checking if no record exist.
            //    if (context.Repository<ProductType>().Read().First() == null)
            //    {
            //        //Create new ProductTypes
            //        await context.Repository<ProductType>().AddRangeAsync(GetProductTypes());
            //    }
            //    if (context.Repository<ProductBrand>().Read().First() == null)
            //    {
            //        //Create new ProductBrands
            //        await context.Repository<ProductBrand>().AddRangeAsync(GetProductBrands());
            //    }
            //}
        }

        private List<ProductType> GetProductTypes()
        {
            var list = new List<ProductType>();
            list.Add(new ProductType
            {
                Name = "Electronics"
            });
            list.Add(new ProductType
            {
                Name = "Computers"
            });
            list.Add(new ProductType
            {
                Name = "Mobile"
            });

            return list;
        }

        private List<ProductBrand> GetProductBrands()
        {
            var list = new List<ProductBrand>();
            list.Add(new ProductBrand
            {
                Name = "Apple"
            });
            list.Add(new ProductBrand
            {
                Name = "Google"
            });
            list.Add(new ProductBrand
            {
                Name = "Microsoft"
            });

            return list;
        }

    }
}
