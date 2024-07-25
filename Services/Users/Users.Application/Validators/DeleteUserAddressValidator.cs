
using eShopping.Constants;
using FluentValidation;
using Users.Application.Commands;

namespace Users.Application.Validators
{
    public class DeleteUserAddressValidator : AbstractValidator<DeleteUserAddressCommand>
    {
        public DeleteUserAddressValidator()
        {
            RuleFor(x => (x.CurrentUser == null || x.CurrentUser.IsInRole(NameConstants.CustomerRoleName) == false))
           .Equal(true).WithMessage("Un-Authorized, access denied.");
            RuleFor(x => x.AddressId).GreaterThan(0).WithMessage("Address Id must not be less than 1.");
        }
    }
}
