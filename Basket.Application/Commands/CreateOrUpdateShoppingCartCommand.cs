
using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public class CreateOrUpdateShoppingCartCommand : IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; } = default!;
        public ShoppingCart ShoppingCart { get; set; } = default!;
    }
}
