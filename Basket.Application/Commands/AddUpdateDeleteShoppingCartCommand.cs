
using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    /// <summary>
    /// Adds the provided shopping cart item to the users shopping cart if it does not exist, or
    /// Updates the users Shopping cart with the provided item if the quantity is greater than zero.
    /// or removes the given item from the shopping cart if the quantity is less than one.
    /// </summary>
    public class AddUpdateDeleteShoppingCartItemCommand : IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; } = default!;
        public ShoppingCartItem ShoppingCartItem { get; set; } = default!;
    }
}
