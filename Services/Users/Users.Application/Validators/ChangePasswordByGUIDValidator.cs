
using FluentValidation;
using Users.Application.Commands;

namespace Users.Application.Validators
{
    public class ChangePasswordByGUIDValidator : AbstractValidator<ChangePasswordByGUIDCommand>
    {
        public ChangePasswordByGUIDValidator()
        {
            RuleFor(x => x.GUID).NotEmpty()
                .WithMessage("GUID must not be empty.");

            RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Password must not be empty.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one numeric character.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        }
    }
}
