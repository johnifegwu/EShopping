
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Extensions;
using Ordering.Application.Mappers;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Handlers
{
    public class UpdateOrderHandlers : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public UpdateOrderHandlers(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<OrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await Task.FromResult(_unitOfWork.Repository<Order>().Get().Where(x => x.Id == request.Payload.Id && x.UserName == request.UserName).FirstOrDefault());
            
            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            await order.UpdateFromRequest(request.Payload);

            await _unitOfWork.Repository<Order>().UpdateAsync(order);

            return OrderingMapper.Mapper.Map<OrderResponse>(order);

        }
    }
}
