
using Data.Repositories;
using eShopping.Exceptions;
using eShopping.MailMan.Interfaces;
using eShopping.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Commands;
using Ordering.Application.Extensions;
using Ordering.Application.Mappers;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using eShopping.Security;
using eShopping.MessageBrocker.Services;
using eShopping.MessageBrocker.Messages;
using eShopping.MessageBrocker.Models;
using eShopping.MessageBrocker.Endpoints;

namespace Ordering.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly DefaultConfig _config;
        private readonly IRabbitMqService _rabbitMq;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(IUnitOfWorkCore unitOfWork, IEmailService emailService, IRabbitMqService rabbitMq, IOptions<DefaultConfig> config, ILogger<CreateOrderHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._emailService = emailService;
            this._config = config.Value;
            this._rabbitMq = rabbitMq;
            this._logger = logger;

            //Must set the communication channel.
            this._rabbitMq.SetEndPoint(NamedEndpoints.OrderBasketQueue);
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = OrderingMapper.Mapper.Map<Order>(request.Payload);

            if (order != null)
            {
                //Proccess Payment.
                if (await order.ProcessPaymentAsync(_config) is false)
                {
                    //Payment failed
                    throw new PaymentFailedException($"Your payment with Card Number:{order.CardNumber.Substring(0, 4)} *** *** {order.CardNumber.Substring(order.CardNumber.Length - 4, order.CardNumber.Length)} failed.");
                }
                else
                {
                    //Encrypt Card number and cvv.
                    order.CardNumber = await order.CardNumber.EncryptString(_config);
                    order.CVV = await order.CVV.EncryptString(_config);
                }

                //Save order.
                await _unitOfWork.Repository<Order>().AddAsync(order, cancellationToken);

                //Save order details
                if (order.OrderDetails != null && order.OrderDetails.Count > 0)
                {
                    //Update order details with the newly aquired order id.
                    order.UpdateChildWithId();

                    //Set Audit fields
                    order.CreatedDate = DateTime.UtcNow;
                    order.CreatedBy = request.CurrentUser.UserName;

                    var orderItems = order.OrderDetails.Select(x => new OrderDetail
                    {
                        OrderId = x.OrderId,
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        Quantity = x.Quantity
                    }).ToList();

                    await _unitOfWork.Repository<OrderDetail>().AddRangeAsync(orderItems, cancellationToken);

                    //Update order
                    order.OrderDetails = orderItems;
                }

                try
                {
                    //Notifiy Customer
                    var emailModel = new OrderEmailModel()
                    {
                        OrderId = order.Id,
                        CustomerEmail = request.CurrentUser.Email,
                        CustomerName = request.CurrentUser.UserName
                    };

                    await _emailService.SendEmailAsync(emailModel.CustomerEmail, $"Order number {order.Id} Created successfully : eShopping", eShopping.Constants.NameConstants.OrderPlacedEmailTemplate, emailModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }

                try
                {
                    //Remove items from Shopping Cart via RabbitMQ
                    await _rabbitMq.StartAsync();

                    await _rabbitMq.SendMessage<OrderBasketMessage>(new OrderBasketMessage
                    {
                        Payload = new OrderBasketModel
                        {
                            UserName = request.CurrentUser.UserName,
                            Products = order.OrderDetails.Select(x => x.ProductId).ToList()
                        }
                    });

                    await _rabbitMq.StopAsync();
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
