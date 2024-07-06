
using Basket.Application.Commands;
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

            //Create Shopping Cart if it does not exist.
            if (cart == null)
            {
                //ToDo: I will be calling Discount Service and aply coupons.
                //===============================================

                //===============================================
                cart = await _unitOfWork.Repository<ShoppingCart>().AddAsync(request.ShoppingCart, request.UserName, cancellationToken);
            }
            else
            {
                cart = await _unitOfWork.Repository<ShoppingCart>().UpdateAsync(request.ShoppingCart, request.UserName, cancellationToken);
            }
           
            return BasketMapper.Mapper.Map<ShoppingCartResponse>(cart);
        }
    }
}
