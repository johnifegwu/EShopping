
using eShopping.Constants;
using FluentValidation;
using Ordering.Application.Queries;

namespace Ordering.Application.Validators
{
    public class GetOrderByGuidValidator : AbstractValidator<GetOrderByGuidQuery>
    {
        public GetOrderByGuidValidator()
        {
            RuleFor(x => (x.CurrentUser == null || x.CurrentUser.IsInRole(NameConstants.AdminRoleName) == false)).Equal(true).WithMessage("Un-Authorized, access denied.");
            RuleFor(x => x.OrderGuid).NotEmpty().WithMessage("Order guid is required.");
        }
    }
}
