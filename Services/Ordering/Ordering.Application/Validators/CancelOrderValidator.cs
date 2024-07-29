
using eShopping.Constants;
using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderValidator()
        {
            RuleFor(x => (x.CurrentUser == null || x.CurrentUser.IsInRole(NameConstants.AdminRoleName) == false)).Equal(true).WithMessage("Un-Authorized, access denied.");
            RuleFor(x => x.Payload.OrderId).GreaterThan(0).WithMessage("Order Id must be greater than zero");
            RuleFor(x => x.Payload.OwnerUserName).NotEmpty().WithMessage("User name of the owner of this order not provided.");
            RuleFor(x => x.Payload.OwnerEmail).NotEmpty().WithMessage("Owner Email not provided")
                .EmailAddress().WithMessage("Owner Email must be a valid email address.");
        }
    }
}
