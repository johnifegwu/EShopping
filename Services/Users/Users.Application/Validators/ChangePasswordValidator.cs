using FluentValidation;
using Users.Application.Commands;

namespace Users.Application.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentUser).NotNull().WithMessage("UserClaims must not be null.")
                .NotEmpty().WithMessage("UserName must not be empty.");
            RuleFor(x => x.Payload.OldPassword).NotEmpty().WithMessage("Old password must not be empty.");
            RuleFor(x => x.Payload.NewPassword)
            .NotEmpty().WithMessage("Password must not be empty.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one numeric character.")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Password must contain at least one special character.")
            .Matches(@"^[^£# “”]*$").WithMessage("Password must not contain the following characters £ # “” or spaces.");
        }
    }
}
