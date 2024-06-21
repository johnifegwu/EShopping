

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
                var productType = context.Repository<ProductType>().Read().First();
                var productBrand = context.Repository<ProductBrand>().Read().First();

                //Initialize Catalog database here after checking if no record exist.
                if (productType  == null)
                {
                    //Create new ProductTypes
                    var productTypes = GetProductTypes();
                    await context.Repository<ProductType>().AddRangeAsync(productTypes);
                    productType = productTypes.FirstOrDefault();
                }

                if (productBrand == null)
                {
                    var productBrands = GetProductBrands();
                    //Create new ProductBrands
                    await context.Repository<ProductBrand>().AddRangeAsync(productBrands);
                    productBrand = productBrands.FirstOrDefault();
                }
                if(productType != null && productBrand != null)
                {
                    try
                    {
                        var product = context.Repository<Product>().Read().First();
                        if (product == null)
                        {
                            //Create new Product
                            product = new Product()
                            {
                                Name = "MXP Pro-Sound System",
                                Summary = "World class sound system.",
                                Description = "MXP Pro-Sound System\nAn award winning World Class Sound System.",
                                Price = 1000,
                                ProductTypeId = productType.Id,
                                ProductBrandId = productBrand.Id
                            };
                            await context.Repository<Product>().AddAsync(product);
                        }
                    }
                    catch(InvalidOperationException ex)
                    {
                        //Create new Product
                        var product = new Product()
                        {
                            Name = "MXP Pro-Sound System",
                            Summary = "World class sound system.",
                            Description = "MXP Pro-Sound System\nAn award winning World Class Sound System.",
                            Price = 1000,
                            ProductTypeId = productType.Id,
                            ProductBrandId = productBrand.Id
                        };
                        await context.Repository<Product>().AddAsync(product);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                //Create new ProductTypes
                var productTypes = GetProductTypes();
                await context.Repository<ProductType>().AddRangeAsync(productTypes);
                var productType = productTypes.FirstOrDefault();

                //Create new ProductBrands
                var productBrands = GetProductBrands();
                await context.Repository<ProductBrand>().AddRangeAsync(productBrands);
                var productBrand = productBrands.FirstOrDefault();

                //Create new Product
                var product = new Product()
                {
                    Name = "MXP Pro-Sound System",
                    Summary = "World class sound system.",
                    Description = "MXP Pro-Sound System\nAn award winning World Class Sound System.",
                    Price = 1000,
                    ProductTypeId = productType.Id,
                    ProductBrandId = productBrand.Id
                };
                await context.Repository<Product>().AddAsync(product);
            }

            ////For SQL Server
            ////==================================================================================
            //var canconnect = await context.GetContext().Database.CanConnectAsync();
            //if (canconnect)
            //{
            //    var productType = context.Repository<ProductType>().Read().First();
            //    var productBrand = context.Repository<ProductBrand>().Read().First();

            //    //Initialize Catalog database here after checking if no record exist.
            //    if (productType == null)
            //    {
            //        //Create new ProductTypes
            //        var productTypes = GetProductTypes();
            //        await context.Repository<ProductType>().AddRangeAsync(productTypes);
            //        productType = productTypes.FirstOrDefault();
            //    }

            //    if (productBrand == null)
            //    {
            //        var productBrands = GetProductBrands();
            //        //Create new ProductBrands
            //        await context.Repository<ProductBrand>().AddRangeAsync(productBrands);
            //        productBrand = productBrands.FirstOrDefault();
            //    }
            //    if (productType != null && productBrand != null)
            //    {
            //        var product = context.Repository<Product>().Read().First();
            //        if (product == null)
            //        {
            //            //Create new Product
            //            product = new Product()
            //            {
            //                Name = "MXP Pro-Sound System",
            //                Summary = "World class sound system.",
            //                Description = "MXP Pro-Sound System\nAn award winning World Class Sound System.",
            //                Price = 1000,
            //                ProductTypeId = productType.Id,
            //                ProductBrandId = productBrand.Id
            //            };
            //            await context.Repository<Product>().AddAsync(product);
            //        }
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
