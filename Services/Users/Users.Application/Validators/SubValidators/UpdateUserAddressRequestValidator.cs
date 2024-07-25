
using FluentValidation;
using Users.Application.Requests;

namespace Users.Application.Validators.SubValidators
{
    public class UpdateUserAddressRequestValidator : AbstractValidator<UpdateUserAddressRequest>
    {
        public UpdateUserAddressRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("AddressId must be greater than 0.");

            RuleFor(x => x.AddressLine1)
                .NotEmpty().WithMessage("AddressLine1 must not be empty.");

            RuleFor(x => x.AddressLine2)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.AddressLine2))
                .WithMessage("AddressLine2 must not be empty if provided.");

            RuleFor(x => x.City)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.City))
                .WithMessage("City must not be empty if provided.");

            RuleFor(x => x.State)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.State))
                .WithMessage("State must not be empty if provided.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.ZipCode))
                .WithMessage("ZipCode must not be empty if provided.");

            RuleFor(x => x.Country)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.Country))
                .WithMessage("Country must not be empty if provided.");
        }
    }
}
