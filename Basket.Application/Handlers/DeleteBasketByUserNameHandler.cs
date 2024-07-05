
using Basket.Application.Commands;
using Basket.Core.Entities;
using Cache.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameCommand, bool>
    {
        private readonly ICacheUnitOfWork _cacheUnitOfWork;

        public DeleteBasketByUserNameHandler(ICacheUnitOfWork cacheUnitOfWork)
        {
            this._cacheUnitOfWork = cacheUnitOfWork;
        }

        public async Task<bool> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            var result = await _cacheUnitOfWork.Repository<ShoppingCart>().DeleteAsync(request.UserName, cancellationToken);

            return result;
        }
    }
}
