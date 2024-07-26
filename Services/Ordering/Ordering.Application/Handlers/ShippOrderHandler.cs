
using Data.Repositories;
using eShopping.Exceptions;
using eShopping.MailMan.Interfaces;
using eShopping.Models;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly IEmailService _emailService;
        private readonly ILogger<ShippOrderHandler> _logger;

        public ShippOrderHandler(IUnitOfWorkCore unitOfWork, IEmailService emailService, ILogger<ShippOrderHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._emailService = emailService;
            this._logger = logger;
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
            
            try
            {
                //Notifiy Customer
                var emailModel = new OrderEmailModel()
                {
                    OrderId = order.Id,
                    CustomerEmail = request.OwnerEmail,
                    CustomerName = request.OwnerUserName
                };

                await _emailService.SendEmailAsync(emailModel.CustomerEmail, $"Order number {order.Id} has been shipped : eShopping", eShopping.Constants.NameConstants.OrderShippedEmailTemplate, emailModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return OrderingMapper.Mapper.Map<OrderResponse>(order);
        }
    }
}
