
using Basket.Application.Commands;
using Basket.Application.Configurations;
using Basket.Application.Extensions;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Cache.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.Application.Handlers
{
    public class CreateOrUpdateShoppingCartHandler : IRequestHandler<CreateOrUpdateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly ICacheUnitOfWork _unitOfWork;
        private readonly DiscountProtoServiceClient _client;
        private readonly ILogger<AddUpdateDeleteShoppingCartItemHandler> _logger;

        public CreateOrUpdateShoppingCartHandler(ICacheUnitOfWork unitOfWork, DiscountProtoServiceClient client, ILogger<AddUpdateDeleteShoppingCartItemHandler> logger)
        {
            _unitOfWork = unitOfWork;
            this._client = client;
            this._logger = logger;
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
            await request.ShoppingCart.ApplyCoupons(exemptList, _client, _logger);

            //This will delete the old cart if it exit and save the new one.
            await _unitOfWork.Repository<ShoppingCart>().UpdateAsync(cart, request.UserName, cancellationToken);
            
            return BasketMapper.Mapper.Map<ShoppingCartResponse>(cart);
        }
    }
}
