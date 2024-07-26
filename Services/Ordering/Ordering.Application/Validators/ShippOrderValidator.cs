
using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class ShippOrderValidator : AbstractValidator<ShippOrderCommand>
    {
        public ShippOrderValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Order Id must be greater than zero");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name not provided.");
            RuleFor(x => x.OwnerUserName).NotEmpty().WithMessage("User name of the owner of this order not provided.");
            RuleFor(x => x.OwnerEmail).NotEmpty().WithMessage("Owner Email not provided")
    .EmailAddress().WithMessage("Owner Email must be a valid email address.");
            RuleFor(x => x.ShiipingDetails).NotEmpty().WithMessage("Shipping details not provided.");
        }
    }
}
