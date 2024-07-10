using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands
{
    public class CreateCouponCommand : IRequest<DiscountModel>
    {
        public CreateDiscountModel Payload { get; set; } = default!;
    }
}
