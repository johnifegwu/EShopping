
using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Cache.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly ICacheUnitOfWork _unitOfWork;

        public CreateShoppingCartHandler(ICacheUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //ToDo: I will be calling Discount Service and aply coupons.
            //===============================================

            //===============================================
            var cart = await _unitOfWork.Repository<ShoppingCart>().AddAsync(request.ShoppingCart, request.UserName, cancellationToken);
            return BasketMapper.Mapper.Map<ShoppingCartResponse>(cart);
        }
    }
}
