using Discount.Application.Requests;
using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Commands
{
    public class CreateCouponCommand : IRequest<CouponResponse>
    {
        public CreateCouponRequest Payload { get; set; } = default!;
    }
}
