
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
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
            var orders = await Task.FromResult(
                (from o in _unitOfWork.Repository<Order>().Read()
                 where o.UserName == request.UserName
                 orderby o.LastModifiedDate descending
                 select new
                 {
                     Order = o,
                     OrderDetails = _unitOfWork.Repository<OrderDetail>().Read().Where(x => x.OrderId == o.Id).ToList()
                 })
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList());

            if(orders.Count > 0)
            {
                foreach (var item in orders)
                {
                    item.Order.OrderDetails = item.OrderDetails;
                }
                return  OrderingMapper.Mapper.Map<IList<OrderResponse>>(orders.Order());
            }

            throw new NotFoundException($"No order found for {request.UserName}.");
        }
    }
}
