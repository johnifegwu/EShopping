
using FluentValidation;
using Users.Application.Queries;

namespace Users.Application.Validators
{
    public class GetUserAddressesValidator : AbstractValidator<GetUserAddressesQuery>
    {
        public GetUserAddressesValidator()
        {
            RuleFor(x => x.CurrentUser).NotNull().WithMessage("CurrentUser must be null.");
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("PageIndex must not be less than 1.");
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageIndex must not be less than 1.");
        }
    }
}
