
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands
{
    public class UpdateCouponCommand : IRequest<DiscountModel>
    {
        public DiscountModel Payload { get; set; } = default!;
    }
}
