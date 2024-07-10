
using MediatR;

namespace Discount.Application.Commands
{
    public class DeleteCouponCommand : IRequest<bool>
    {
        public string ProductId { get; set; } = default!;
    }
}
