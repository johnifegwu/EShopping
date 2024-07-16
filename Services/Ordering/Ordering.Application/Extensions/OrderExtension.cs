
using Data.Repositories;
using MediatR;
using Ordering.Application.Mappers;
using Ordering.Application.Requests;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Extensions
{
    internal static class OrderExtension
    {
        public static async Task<IList<OrderResponse>> GetOrdersByUserName(this IUnitOfWorkCore _unitOfWork, string UserName, int PageIndex, int PageSize)
        {
            var orders = await Task.FromResult(
                (from o in _unitOfWork.Repository<Order>().Read()
                 where o.UserName == UserName
                 orderby o.LastModifiedDate descending
                 select new
                 {
                   Order = o,
                   OrderDetails = _unitOfWork.Repository<OrderDetail>().Read().Where(x => x.OrderId == o.Id).ToList()
                })
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize).ToList());

            if (orders.Count > 0)
            {
                foreach (var item in orders)
                {
                    item.Order.OrderDetails = item.OrderDetails;
                }
                return OrderingMapper.Mapper.Map<IList<OrderResponse>>(orders.Order());
            }

            return new List<OrderResponse>();
        }
    }
}
