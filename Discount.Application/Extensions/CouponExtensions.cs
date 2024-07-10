
using Data.Repositories;
using Discount.Core.Entities;

namespace Discount.Application.Extensions
{
    public static class CouponExtensions
    {
        public static async Task<bool> IsCouponExist(this Coupon coupon, IUnitOfWorkCore _unitOfWork)
        {
            var result = await Task.FromResult(_unitOfWork.Repository<Coupon>().Read().Where(x => x.ProductId == coupon.ProductId).FirstOrDefault());   

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> IsCouponExist(this string productId, IUnitOfWorkCore _unitOfWork)
        {
            var result = await Task.FromResult(_unitOfWork.Repository<Coupon>().Read().Where(x => x.ProductId == productId).FirstOrDefault());

            if (result == null)
            {
                return false;
            }

            return true;
        }
    }
}
