
using eShopping.Constants;
using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class DeleteOrderValidation : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderValidation()
        {
            RuleFor(x => (x.CurrentUser == null || x.CurrentUser.IsInRole(NameConstants.AdminRoleName) == false)).Equal(true).WithMessage("Un-Authorized, access denied.");
            RuleFor(x => x.Payload.OrderId).GreaterThan(0).WithMessage("Order Id must be greater than zero");
            RuleFor(x => x.Payload.OrderUserName).NotEmpty().WithMessage("Order owners UserName is required.");
        }
    }
}
