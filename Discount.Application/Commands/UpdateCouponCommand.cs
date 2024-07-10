
using Discount.Application.Requests;
using Discount.Application.Responses;
using MediatR;

namespace Discount.Application.Commands
{
    public class UpdateCouponCommand : IRequest<CouponResponse>
    {
        public UpdateCouponRequest Payload { get; set; } = default!;
    }
}
