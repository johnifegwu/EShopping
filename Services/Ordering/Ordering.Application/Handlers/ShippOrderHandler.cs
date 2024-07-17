
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Extensions;
using Ordering.Application.Mappers;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Exceptions;

namespace Ordering.Application.Handlers
{
    public class ShippOrderHandler : IRequestHandler<ShippOrderCommand, OrderResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public ShippOrderHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<OrderResponse> Handle(ShippOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.GetOrderById(request.OrderId, request.OwnerUserName);

            if (order == null)
            {
                throw new OrderNotFoundException(request.OrderId, request.OwnerUserName);
            }

            //check if this order has been paid for.
            if(!order.IsPaid is true)
            {
                throw new InvalidOperationException($"Order number {request.OrderId} has not been paid for, operation has been terminated.");
            }

            //Mark order as shipped
            order.IsShipped = true;
            order.LastModifiedBy = request.UserName;
            order.LastModifiedDate = DateTime.UtcNow;
            await _unitOfWork.Repository<Order>().UpdateAsync(order, cancellationToken);

            return OrderingMapper.Mapper.Map<OrderResponse>(order);
        }
    }
}
