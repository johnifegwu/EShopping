
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Cache.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly ICacheUnitOfWork _cacheUnitOfWork;

        public GetBasketByUserNameHandler(ICacheUnitOfWork cacheUnitOfWork)
        {
            _cacheUnitOfWork = cacheUnitOfWork;
        }

        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cacheUnitOfWork.Repository<ShoppingCart>().GetAsync(request.UserName);

            if (cart == null)
            {
                return new ShoppingCartResponse();
            }

            return BasketMapper.Mapper.Map<ShoppingCartResponse>(cart);
        }
    }
}
