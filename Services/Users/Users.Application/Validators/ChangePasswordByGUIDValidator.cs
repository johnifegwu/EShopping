using FluentValidation;
using Users.Application.Commands;

namespace Users.Application.Validators
{
    public class ChangePasswordByGUIDValidator : AbstractValidator<ChangePasswordByGUIDCommand>
    {
        public ChangePasswordByGUIDValidator()
        {
            RuleFor(x => x.Payload.GUID).NotEmpty()
                .WithMessage("GUID must not be empty.");

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
