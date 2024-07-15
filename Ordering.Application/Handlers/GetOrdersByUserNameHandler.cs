
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
using Ordering.Application.Extensions;
using Ordering.Application.Mappers;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Handlers
{
    public class GetOrdersByUserNameHandler : IRequestHandler<GetOrdersByUserNameQuery, IList<OrderResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetOrdersByUserNameHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IList<OrderResponse>> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.GetOrdersByUserName(request.UserName, request.PageIndex, request.PageSize);

            if(result != null || result.Count > 0)
            {
                return result;
            }

            throw new NotFoundException($"No order found for {request.UserName}.");
        }
    }
}
