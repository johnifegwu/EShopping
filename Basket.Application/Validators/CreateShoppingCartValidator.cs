
using Basket.Application.Commands;
using FluentValidation;

namespace Basket.Application.Validators
{
    public class CreateShoppingCartValidator : AbstractValidator<CreateShoppingCartCommand>
    {
        public CreateShoppingCartValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required.");
            RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Shoping cart can not be null.");
            RuleFor(x => x.UserName).Equal(x => x.ShoppingCart.UserName);
            RuleFor(x => x.ShoppingCart.Items).NotNull().WithMessage("Shopping cart items can not be null.");
        }
    }
}
