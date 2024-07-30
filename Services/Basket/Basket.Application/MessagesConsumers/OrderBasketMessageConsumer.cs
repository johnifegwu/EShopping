using MassTransit;
using eShopping.MessageBrocker.Messages;
using Cache.Repositories;
using Basket.Core.Entities;

namespace eShopping.MessageBrocker.MessagesConsumers
{
    public class OrderBasketMessageConsumer : IConsumer<OrderBasketMessage>
    {
        private readonly ICacheUnitOfWork _cacheUnitOfWork;

        public OrderBasketMessageConsumer(ICacheUnitOfWork cacheUnitOfWork)
        {
            this._cacheUnitOfWork = cacheUnitOfWork;
        }

        public async Task Consume(ConsumeContext<OrderBasketMessage> context)
        {
            //Delete Products from Basket here
            //=======================================================

            var basket = await _cacheUnitOfWork.Repository<ShoppingCart>().GetAsync(context.Message.Payload.UserName);

            if(basket is not null && basket.Items.Count > 0)
            {
                var cartItems = new List<ShoppingCartItem>();

                foreach(var item in basket.Items)
                {
                    if (!context.Message.Payload.Products.Contains(item.ProductId))
                    {
                        cartItems.Add(item);
                    }
                }

                if(cartItems.Count > 0)
                {
                    //Update Basket
                    basket.Items = cartItems;
                    await _cacheUnitOfWork.Repository<ShoppingCart>().UpdateAsync(basket, context.Message.Payload.UserName);
                }
                else
                {
                    //Delete basket
                    await _cacheUnitOfWork.Repository<ShoppingCart>().DeleteAsync(context.Message.Payload.UserName);
                }
                

            }


            //======================================================
            await Task.CompletedTask;
        }
    }
}
