using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetCouponByProductIdQuery : IRequest<DiscountModel>
    {
        public string ProductId { get; set; } = default!;
    }
}
