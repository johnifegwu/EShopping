
using Basket.Application.Commands;
using FluentValidation;

namespace Basket.Application.Validators
{
    public class DeleteBasketByUserNameValidator : AbstractValidator<DeleteBasketByUserNameCommand>
    {
        public DeleteBasketByUserNameValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name not provided.");
        }
    }
}
