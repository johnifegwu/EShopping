

using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly IMediator _mediator;

        public DiscountService(ILogger<DiscountService> logger, IMediator mediator)
        {
            this._logger = logger;
            this._mediator = mediator;
        }

        public override async Task<DiscountModel> GetDiscount(GetDiscountRequest request, ServerCallContext callContext)
        {
            var result = await _mediator.Send(new GetCouponByProductIdQuery
            {
                ProductId = request.ProductId,
            });

            _logger.LogInformation($"Get request for {result.ProductName} completed.", request);

            return result;
        }

        public override async Task<DiscountModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext callContext)
        {
            var result = await _mediator.Send(new CreateCouponCommand
            {
                Payload = request.Payload,
            });

            _logger.LogInformation($"Create for {result.ProductName} completed.", request);

            return result;
        }

        public override async Task<DiscountModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new UpdateCouponCommand
            {
                Payload = request.Payload,
            });

            _logger.LogInformation($"Update for {result.ProductName} completed.", request);

            return result;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteCouponCommand
            {
                ProductId = request.ProductId,
            });

            _logger.LogInformation($"Delete for {request.ProductId} completed.", request);

            return new DeleteDiscountResponse
            {
                Success = result,
            };
        }
    }
}
