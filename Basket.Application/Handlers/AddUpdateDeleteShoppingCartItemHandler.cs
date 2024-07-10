
using Basket.Application.Commands;
using Basket.Application.Configurations;
using Basket.Application.Extensions;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Cache.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace Basket.Application.Handlers
{
    public class AddUpdateDeleteShoppingCartItemHandler : IRequestHandler<AddUpdateDeleteShoppingCartItemCommand, ShoppingCartResponse>
    {
        private readonly ICacheUnitOfWork _cacheUnitOfWork;
        private readonly IOptions<DefaultConfig> _config;

        public AddUpdateDeleteShoppingCartItemHandler(ICacheUnitOfWork cacheUnitOfWork, IOptions<DefaultConfig> config)
        {
            this._cacheUnitOfWork = cacheUnitOfWork;
            this._config = config;
        }
        public async Task<ShoppingCartResponse> Handle(AddUpdateDeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cacheUnitOfWork.Repository<ShoppingCart>().GetAsync(request.UserName, cancellationToken);

            //Create Shopping Cart if it does not exist.
            if (cart == null)
            {
                cart = new ShoppingCart(request.UserName);
            }

            var exemptList = cart.Items.Select(x => x.ProductId).ToList();

            /// Adds the provided shopping cart item to the users shopping cart if it does not exist, or
            /// Updates the users Shopping cart with the provided item if the quantity is greater than zero.
            /// or removes the given item from the shopping cart if the quantity is less than one.
            cart.Update(request.ShoppingCartItem);

            //Apply coupons
            await cart.ApplyCoupons(exemptList, _config);

            //This will delete the old cart if it exit and save the new one.
            await _cacheUnitOfWork.Repository<ShoppingCart>().UpdateAsync(cart, request.UserName, cancellationToken);

            return BasketMapper.Mapper.Map<ShoppingCartResponse>(cart);
        }
    }
}
