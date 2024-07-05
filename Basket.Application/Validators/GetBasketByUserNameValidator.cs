
using Basket.Application.Queries;
using FluentValidation;

namespace Basket.Application.Validators
{
    public class GetBasketByUserNameValidator : AbstractValidator<GetBasketByUserNameQuery>
    {
        public GetBasketByUserNameValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
        }
    }
}
