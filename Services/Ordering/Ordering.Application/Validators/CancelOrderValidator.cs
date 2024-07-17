
using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Order Id must be greater than zero");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name not provided.");
            RuleFor(x => x.OwnerUserName).NotEmpty().WithMessage("User name of the owner of this order not provided.");
        }
    }
}
