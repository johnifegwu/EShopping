﻿
using eShopping.Constants;
using FluentValidation;
using Users.Application.Commands;
using Users.Application.Validators.SubValidators;

namespace Users.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => ((x.IsAdminUser == true) && (x.CurrentUser == null || !x.CurrentUser.IsInRole(NameConstants.AdminRoleName))))
                .Equal(true).WithMessage("Un-Authorized, access denied.");
            
            // Validate Payload
            RuleFor(x => x.Payload)
                .NotNull().WithMessage("Payload must not be null.");

            RuleFor(x => x.Payload.UserName)
                .NotEmpty().WithMessage("UserName must not be empty.");

            RuleFor(x => x.Payload.UserEmail)
                .NotEmpty().WithMessage("UserEmail must not be empty.")
                .EmailAddress().WithMessage("UserEmail must be a valid email address.");

            RuleFor(x => x.Payload.Password)
            .NotEmpty().WithMessage("Password must not be empty.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one numeric character.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleForEach(x => x.Payload.Addresses).SetValidator(new CreateUserAddressRequestValidator());
        }
    }

}
