
using Data.Repositories;
using eShopping.Exceptions;
using eShopping.MailMan.Interfaces;
using eShopping.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Mappers;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(IUnitOfWorkCore unitOfWork, IEmailService emailService, ILogger<CreateOrderHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._emailService = emailService;
            this._logger = logger;
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

                try
                {
                    //Notifiy Customer
                    var emailModel = new OrderEmailModel()
                    {
                        OrderId = order.Id,
                        CustomerEmail = request.UserEmail,
                        CustomerName = request.UserName
                    };

                    await _emailService.SendEmailAsync(emailModel.CustomerEmail, $"Order number {order.Id} Created successfully : eShopping", eShopping.Constants.NameConstants.OrderPlacedEmailTemplate, emailModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }

                return OrderingMapper.Mapper.Map<OrderResponse>(order);
            }

            throw new BadRequestException("Order creation failed.");
        }
    }
}
