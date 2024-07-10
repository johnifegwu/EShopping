
using Discount.Application.Queries;
using FluentValidation;

namespace Discount.Application.Validators
{
    public class GetCouponByProductIdValidator : AbstractValidator<GetCouponByProductIdQuery>
    {
        public GetCouponByProductIdValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id not provided.");
        }
    }
}
