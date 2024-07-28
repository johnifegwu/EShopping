
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
using Ordering.Application.Extensions;
using Ordering.Application.Queries;
using Ordering.Application.Responses;

namespace Ordering.Application.Handlers
{
    public class GetOrderByGuidHandler : IRequestHandler<GetOrderByGuidQuery, OrderResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetOrderByGuidHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<OrderResponse> Handle(GetOrderByGuidQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetOrderByGuid(request.OrderGuid);

            if (result is not null)
            {
                return result;
            }

            throw new NotFoundException($"No order found for {request.OrderGuid}.");
        }
    }
}
