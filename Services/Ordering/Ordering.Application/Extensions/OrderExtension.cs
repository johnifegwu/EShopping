
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
        /// Gets customer Order by Id.
        /// </summary>
        /// <param name="_unitOfWork">IUnitOfWork object.</param>
        /// <param name="OrderId">Order id.</param>
        /// <returns>Order object.</returns>
        public static async Task<Order> GetOrderByIdWithDetails(this IUnitOfWorkCore _unitOfWork, int OrderId, string UserName)
        {
            var order = await Task.FromResult(
               (from o in _unitOfWork.Repository<Order>().Get()
                where o.Id == OrderId && o.UserName == UserName
                select new
                {
                    Order = o,
                    OrderDetails = _unitOfWork.Repository<OrderDetail>().Read().Where(x => x.OrderId == o.Id).ToList()
                }).FirstOrDefault());

            if (order != null)
            {
                order.Order.OrderDetails = order.OrderDetails;
                return order.Order;
            }

            return null;
        }


        /// <summary>
        /// An extention for updating Order fields with the provided changes.
        /// </summary>
        /// <param name="order">Order object.</param>
        /// <param name="request">Changes to be effected.</param>
        /// <returns>The updated order.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<Order> UpdateFromRequest(this Order order, UpdateOrderRequest request)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Update order fields with request
            order.LastModifiedBy = request.UserName;
            order.LastModifiedDate = DateTime.UtcNow;
            order.UserName = request.UserName;
            order.TotalPrice = request.TotalPrice;
            order.FirstName = request.FirstName;
            order.LastName = request.LastName;
            order.EmailAddress = request.EmailAddress;
            order.AddressLine1 = request.AddressLine1;
            order.AddressLine2 = request.AddressLine2;
            order.City = request.City;
            order.State = request.State;
            order.ZipCode = request.ZipCode;
            order.Country = request.Country;
            order.CardName = request.CardName;
            order.CardNumber = request.CardNumber;
            order.CardType = request.CardType;
            order.Expiration = request.Expiration;
            order.CVV = request.CVV;
            order.PaymentMethod = request.PaymentMethod;

            return order;
        }
    }
}
