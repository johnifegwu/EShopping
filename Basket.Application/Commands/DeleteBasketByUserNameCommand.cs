using MediatR;

namespace Basket.Application.Commands
{
    public class DeleteBasketByUserNameCommand : IRequest<bool>
    {
        public string UserName { get; set; } = default!;
    }
}
