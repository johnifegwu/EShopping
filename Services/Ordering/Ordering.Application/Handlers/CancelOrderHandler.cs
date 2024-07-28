
using Data.Repositories;
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
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, OrderResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<CancelOrderHandler> _logger;

        public CancelOrderHandler(IUnitOfWorkCore unitOfWork, IEmailService emailService, ILogger<CancelOrderHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._emailService = emailService;
            _logger = logger;
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
            order.LastModifiedBy = request.CurrentUser.UserName;
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

                await _emailService.SendEmailAsync(emailModel.CustomerEmail, $"Order number {order.Id} Canceled : eShopping", eShopping.Constants.NameConstants.OrderCanceledEmailTemplate, emailModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return OrderingMapper.Mapper.Map<OrderResponse>(order);
        }
    }
}
