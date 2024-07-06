
using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Cache.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class AddUpdateDeleteShoppingCartItemHandler : IRequestHandler<AddUpdateDeleteShoppingCartItemCommand, ShoppingCartResponse>
    {
        private readonly ICacheUnitOfWork _cacheUnitOfWork;

        public AddUpdateDeleteShoppingCartItemHandler(ICacheUnitOfWork cacheUnitOfWork)
        {
            this._cacheUnitOfWork = cacheUnitOfWork;
        }
        public async Task<ShoppingCartResponse> Handle(AddUpdateDeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cacheUnitOfWork.Repository<ShoppingCart>().GetAsync(request.UserName, cancellationToken);
            
            //Create Shopping Cart if it does not exist.
            if (cart == null)
            {
                cart = new ShoppingCart(request.UserName);

                //ToDo: I will be calling Discount Service and aply coupons to shopping cart item.
                //===============================================

                //===============================================
            }

            cart.Update(request.ShoppingCartItem);
            await _cacheUnitOfWork.Repository<ShoppingCart>().UpdateAsync(cart, request.UserName, cancellationToken);

            return BasketMapper.Mapper.Map<ShoppingCartResponse>(cart);
        }
    }
}
