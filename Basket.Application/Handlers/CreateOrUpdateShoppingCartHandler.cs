
using Basket.Application.Commands;
using Basket.Application.Extensions;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Cache.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateOrUpdateShoppingCartHandler : IRequestHandler<CreateOrUpdateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly ICacheUnitOfWork _unitOfWork;

        public CreateOrUpdateShoppingCartHandler(ICacheUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ShoppingCartResponse> Handle(CreateOrUpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Repository<ShoppingCart>().GetAsync(request.UserName, cancellationToken);
            
            var exemptList = new List<string>();

            if (cart != null)
            {
                exemptList = cart.Items.Select(x => x.ProductId).ToList();
            }

            //Update old Shopping cart items with the new Shopping cart items.
            cart = (cart != null)? await cart.UpdateCart(request.ShoppingCart) : request.ShoppingCart;
            
            //Apply coupons
            await request.ShoppingCart.ApplyCoupons(exemptList);

            //This will delete the old cart if it exit and save the new one.
            await _unitOfWork.Repository<ShoppingCart>().UpdateAsync(cart, request.UserName, cancellationToken);
            
            return BasketMapper.Mapper.Map<ShoppingCartResponse>(cart);
        }
    }
}
