
using Discount.Application.Commands;
using FluentValidation;

namespace Discount.Application.Validators
{
    public class CreateCouponValidation : AbstractValidator<CreateCouponCommand>
    {
        public CreateCouponValidation()
        {
            RuleFor(x => x.Payload).NotNull().WithMessage("Payload can not be null.");
            RuleFor(x => x.Payload.Amount).GreaterThanOrEqualTo(0).WithMessage("Amount can not be less than zero.");
            RuleFor(x => x.Payload.ProductId).NotEmpty().WithMessage("Product Id not provided.");
            RuleFor(x => x.Payload.ProductName).NotEmpty().WithMessage("Product name not provided.");
        }
    }
}
