
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Mappers;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public CreateOrderHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = OrderingMapper.Mapper.Map<Order>(request);

            if (order != null)
            {
                //Save order.
                await _unitOfWork.Repository<Order>().AddAsync(order, cancellationToken);

                //Save order details
                if(order.OrderDetails != null && order.OrderDetails.Count > 0)
                {
                    //Update order details with the newly aquired order id.
                    order.UpdateChildWithId();

                    //Set Audit fields
                    order.CreatedDate = DateTime.UtcNow;
                    order.CreatedBy = request.UserName;

                    await _unitOfWork.Repository<OrderDetail>().AddRangeAsync(order.OrderDetails, cancellationToken);
                }

                return OrderingMapper.Mapper.Map<OrderResponse>(order);
            }

            throw new BadRequestException("Order creation failed.");
        }
    }
}
