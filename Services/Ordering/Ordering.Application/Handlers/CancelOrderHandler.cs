
using Data.Repositories;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Extensions;
using Ordering.Application.Mappers;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Exceptions;

namespace Ordering.Application.Handlers
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, OrderResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public CancelOrderHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<OrderResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.GetOrderById(request.OrderId, request.OwnerUserName);

            if (order == null)
            {
                throw new OrderNotFoundException(request.OrderId, request.OwnerUserName);
            }

            if ((order.IsShipped is true))
            {
                throw new OrderInProcessException(request.OrderId, true);
            }

            //Mark oredr as canceled
            order.IsCanceled = true;
            order.LastModifiedBy = request.UserName;
            order.LastModifiedDate = DateTime.UtcNow;
            await _unitOfWork.Repository<Order>().UpdateAsync(order, cancellationToken);

            return OrderingMapper.Mapper.Map<OrderResponse>(order);
        }
    }
}
