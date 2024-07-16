
using Basket.Application.Commands;
using FluentValidation;

namespace Basket.Application.Validators
{
    public class AddUpdateDeleteShoppingCartItemValidator : AbstractValidator<AddUpdateDeleteShoppingCartItemCommand>
    {
        public AddUpdateDeleteShoppingCartItemValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username not provided.");
            RuleFor(x => x.ShoppingCartItem).NotNull().WithMessage("Shopping cart item can not be null.");
            RuleFor(x => x.ShoppingCartItem.ProductId).NotEmpty().WithMessage("Product Id not provided.");
            RuleFor(x => x.ShoppingCartItem.ProductName).NotEmpty().WithMessage("Product Name not provided.");
            RuleFor(x => x.ShoppingCartItem.Price).GreaterThanOrEqualTo(0).WithMessage("Price can not be less than zero.");
            RuleFor(x => x.ShoppingCartItem.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity can not be less than zero.");
        }
    }
}
