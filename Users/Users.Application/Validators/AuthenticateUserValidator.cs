
using FluentValidation;
using Users.Application.Commands;

namespace Users.Application.Validators
{
    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(x => x.Payload.UserName).NotEmpty().WithMessage("Username not provided.");
            RuleFor(x => x.Payload.Password).NotEmpty().WithMessage("Password not provided.");
        }
    }
}
