
using FluentValidation;
using Users.Application.Commands;

namespace Users.Application.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName must not be empty.");
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Old password must not be empty.");
            RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password must not be empty.")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
            .Matches("[0-9]").WithMessage("New password must contain at least one numeric character.")
            .Matches("[^a-zA-Z0-9]").WithMessage("New password must contain at least one special character.");
        }
    }
}
