
using eShopping.Constants;
using eShopping.Models;
using FluentValidation;
using Microsoft.Extensions.Options;
using Users.Application.Commands;
using Users.Application.Validators.SubValidators;

namespace Users.Application.Validators
{
    public class AddUserAddressValidator : AbstractValidator<AddUserAddressCommand>
    {
        public AddUserAddressValidator()
        {
            RuleFor(x => (x.CurrentUser == null || x.CurrentUser.IsInRole(NameConstants.CustomerRoleName) == false))
                .Equal(true).WithMessage("Un-Authorized, access denied.");
            RuleFor(x => x.Payload).SetValidator(new CreateUserAddressRequestValidator());
        }
    }
}
