using Basket.Application.Configurations;
using Basket.Application.RpcClients;
using Basket.Core.Entities;
using Microsoft.Extensions.Options;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.Application.Extensions
{
    public static class ShoppingCartExtension
    {

        /// <summary>
        /// Apply coupons to the provided ShoppingCart.
        /// </summary>
        /// <param name="cart">Shopping cart.</param>
        /// <param name="exemptionList">A list of Product Ids to exclude.</param>
        /// <returns>The updataed ShoppingCart.</returns>
        public static async Task<ShoppingCart> ApplyCoupons(this ShoppingCart cart, List<string> exemptionList, DiscountProtoServiceClient client)
        {
            if (cart != null)
            {
                if(exemptionList == null)
                    exemptionList = new List<string>();

                foreach (var item in cart.Items)
                {
                    if (!exemptionList.Contains(item.ProductId)){

                        //Call Coupon Service and get all relevant coupons here
                        //==============================================================================

                        var discount = DiscountRpcClient.GetDiscountAsync(item.ProductId, client).Result;

                        //==============================================================================
                        //ToDo: Apply coupons here


                    }
                }
            }

            return cart;
        }


        /// <summary>
        /// Updates the old Shopping cart with items from the new Shopping cart.
        /// </summary>
        /// <param name="oldCart">Old Shopping Cart.</param>
        /// <param name="newCart">New Shopping Cart.</param>
        /// <returns></returns>
        public static async Task<ShoppingCart> UpdateCart(this ShoppingCart oldCart, ShoppingCart newCart)
        {
            if (oldCart == null)
            {
                oldCart = newCart;

                return oldCart;
            }

            //Update old Shopping cart items with the new Shopping cart items.
            foreach (var item in newCart.Items)
            {
                /// Adds the provided shopping cart item to the users shopping cart if it does not exist, or
                /// Updates the users Shopping cart with the provided item if the quantity is greater than zero.
                /// or removes the given item from the shopping cart if the quantity is less than one.
                oldCart.Update(item);
            }

            return oldCart;
        }
    }
}
