
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

        /// <summary>
        /// Gets all the orders that belongs to the given user.
        /// </summary>
        /// <param name="_unitOfWork">IUnitOfWork object.</param>
        /// <param name="UserName">Current user.</param>
        /// <param name="PageIndex">Page index.</param>
        /// <param name="PageSize">Page size.</param>
        /// <returns>A list of OrderResponse object.</returns>
        public static async Task<IList<OrderResponse>> GetOrdersByUserName(this IUnitOfWorkCore _unitOfWork, string UserName, int PageIndex, int PageSize)
        {
            var orders = await Task.FromResult(
                (from o in _unitOfWork.Repository<Order>().Read()
                 where o.UserName == UserName
                 orderby o.LastModifiedDate descending
                 select new OrderQueryResponse
                 {
                   Order = o,
                   Details = _unitOfWork.Repository<OrderDetail>().Read().Where(x => x.OrderId == o.Id).ToList()
                })
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize).ToList());

            if (orders.Count > 0)
            {
                foreach (var item in orders)
                {
                    item.MapOrderWithDetails();
                }
                return OrderingMapper.Mapper.Map<IList<OrderResponse>>(orders.Order());
            }

            return new List<OrderResponse>();
        }

        /// <summary>
        /// Gets customer Order by Id.
        /// </summary>
        /// <param name="_unitOfWork">IUnitOfWork object.</param>
        /// <param name="OrderId">Order id.</param>
        /// <param name="UserName">Username of the owner of the order to fetch.</param>
        /// <returns>Order object.</returns>
        public static async Task<Order> GetOrderById(this IUnitOfWorkCore _unitOfWork, int OrderId, string UserName)
        {
            var order = await Task.FromResult(
               (from o in _unitOfWork.Repository<Order>().Get()
                where o.Id == OrderId && o.UserName == UserName
                select o).FirstOrDefault()
               );

            return order;
        }

        /// <summary>
        /// Gets customer Order by Guid.
        /// </summary>
        /// <param name="_unitOfWork">IUnitOfWork object.</param>
        /// <param name="OrderGuid">Order Guid.</param>
        /// <param name="UserName">Username of the owner of the order to fetch.</param>
        /// <returns>Order object.</returns>
        public static async Task<Order> GetOrderByGuid(this IUnitOfWorkCore _unitOfWork, string OrderGuid, string UserName)
        {
            var order = await Task.FromResult(
               (from o in _unitOfWork.Repository<Order>().Get()
                where o.OrderGuid == OrderGuid && o.UserName == UserName
                select o).FirstOrDefault()
               );

            return order;
        }


        /// <summary>
        /// Gets Orders from the system according to the provided search parameters.
        /// </summary>
        /// <param name="_unitOfWork">UnitWork object.</param>
        /// <param name="OptionalUserName">Username (optional).</param>
        /// <param name="IsShipped">If the order is shipped or not.</param>
        /// <param name="IsPaid">If the order has been paid for or not.</param>
        /// <param name="IsCanceled">If the order has been canceled or not.</param>
        /// <param name="IsDeleted">If the order has been marked as deleted or not.</param>
        /// <param name="PageIndex">Page index.</param>
        /// <param name="PageSize">Page size.</param>
        /// <returns></returns>
        public static async Task<IList<OrderResponse>> GetOrdersByFlags(this IUnitOfWorkCore _unitOfWork, string? OptionalUserName, bool IsShipped, bool IsPaid, bool IsCanceled, bool IsDeleted, int PageIndex, int PageSize)
        {
            var query = _unitOfWork.Repository<Order>().Read()
            .Where(o => o.IsShipped == IsShipped
                    && o.IsPaid == IsPaid
                    && o.IsCanceled == IsCanceled
                    && o.IsDeleted == IsDeleted);

            if (!string.IsNullOrEmpty(OptionalUserName))
            {
                query = query.Where(o => o.UserName == OptionalUserName);
            }

            var orders = await Task.FromResult(query
                .OrderBy(o => o.LastModifiedDate)
                .Select(o => new OrderQueryResponse
                {
                    Order = o,
                    Details = _unitOfWork.Repository<OrderDetail>().Read().Where(x => x.OrderId == o.Id).ToList()
                })
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToList());

            if (orders.Count > 0)
            {
                foreach (var item in orders)
                {
                    item.MapOrderWithDetails();
                }
                return OrderingMapper.Mapper.Map<IList<OrderResponse>>(orders.Order());
            }

            return new List<OrderResponse>();
        }
    }

    public class OrderQueryResponse
    {
        public Order Order { get; set; } = default!;
        public List<OrderDetail> Details { get; set; } = default!;

        public Order MapOrderWithDetails()
        {
            if (Order != null)
            {
                Order.OrderDetails = Details;
            }

            return Order;
        }
    }
}
