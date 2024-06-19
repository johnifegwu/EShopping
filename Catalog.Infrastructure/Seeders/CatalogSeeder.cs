

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
                //ToDo: Initialize Catalog database here after checking if no record exist.
                if (context.Repository<TestCollection>().Read().First() == null)
                {
                    //Create new TestCollections
                    await context.Repository<TestCollection>().AddRangeAsync(GetTestCollections());
                }
            }
            catch (Exception ex)
            {
                //Create new TestCollections
                await context.Repository<TestCollection>().AddRangeAsync(GetTestCollections());
            }

            //For SQL Server
            //==================================================================================
            //var canconnect = await context.GetContext().Database.CanConnectAsync();
            //if (canconnect)
            //{
            //    //ToDo: Initialize Catalog database here after checking if no record exist.
            //    if (context.Repository<TestCollection>().Read().First() == null)
            //    {
            //        //Create new TestCollections
            //        await context.Repository<TestCollection>().AddRangeAsync(GetTestCollections());
            //    }
            //}
        }

        private List<TestCollection> GetTestCollections()
        {
            var list = new List<TestCollection>();
            list.Add(new TestCollection
            {
                Name = "Test1",
            });
            list.Add(new TestCollection
            {
                Name = "Test2",
            });

            return list;
        }


        internal partial class TestCollection : BaseEntity
        {
            public string Name { get; set; } = default!;
        }
    }
}
