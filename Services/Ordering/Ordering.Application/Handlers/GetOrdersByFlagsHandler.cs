using Data.Repositories;
using MediatR;
using Ordering.Application.Extensions;
using Ordering.Application.Queries;
using Ordering.Application.Responses;

namespace Ordering.Application.Handlers
{
    public class GetOrdersByFlagsHandler : IRequestHandler<GetOrdersByFlagsQuery, IList<OrderResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetOrdersByFlagsHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IList<OrderResponse>> Handle(GetOrdersByFlagsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetOrdersByFlags(request.OptionalUserName,request.IsShipped, request.IsPaid, request.IsCanceled, request.IsDeleted, request.PageIndex, request.PageSize);
        }
    }
}
