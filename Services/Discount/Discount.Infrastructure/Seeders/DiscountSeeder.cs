
using Data.Repositories;
using Discount.Core.Entities;

namespace Discount.Infrastructure.Seeders
{
    internal class DiscountSeeder(IUnitOfWorkCore context) : IDiscountSeeder
    {
        public async Task Seed()
        {
            ////==================================================================================
            var canconnect = await context.GetContext().Database.CanConnectAsync();

            if (canconnect)
            {
                var coupon = context.Repository<Coupon>().Read().First();

                if (coupon == null)
                {
                    var coupons = GetCoupon();
                    await context.Repository<Coupon>().AddRangeAsync(coupons);
                    coupon = coupons.FirstOrDefault();
                }
            }

            List<Coupon> GetCoupon()
            {
                return new List<Coupon>
                {
                    new Coupon()
                    {
                        ProductName = "MXP Pro-Sound System",
                        Description = "MXP Pro-Sound System",
                        Amount = 100,
                        ProductId = "668029f91e14c6c6e38205f9"
                    },
                    new Coupon()
                    {
                        ProductName = "TSQ 100 Pro-Sound System",
                        Description = "TSQ 100 Pro-Sound System",
                        Amount = 200,
                        ProductId = "66802d3e1e14c6c6e38205fa"
                    }
                };
            }
        }
    }
}
