
using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetCouponByProductIdQuery : IRequest<CouponResponse>
    {
        public string ProductId { get; set; } = default!;
    }
}
