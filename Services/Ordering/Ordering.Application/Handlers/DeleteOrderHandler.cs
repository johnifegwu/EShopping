
using Data.Repositories;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Extensions;
using Ordering.Core.Entities;
using Ordering.Core.Exceptions;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public DeleteOrderHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.GetOrderById(request.OrderId, request.UserName);

            if (order == null)
            {
                throw new OrderNotFoundException(request.OrderId, request.UserName);
            }

            if(order.IsPaid is true || order.IsShipped is true)
            {
                if ((order.IsShipped is true))
                {
                    throw new OrderInProcessException(request.OrderId, true);
                }
                else
                {
                    throw new OrderInProcessException();
                }
            }

            //mark order as deleted
            order.IsCanceled = true;
            order.IsDeleted = true;
            order.LastModifiedBy = request.UserName;
            order.LastModifiedDate = DateTime.UtcNow;
            var result = await _unitOfWork.Repository<Order>().UpdateAsync(order, cancellationToken);

            return true;
        }
    }
}
