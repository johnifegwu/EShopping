
using Discount.Application.Commands;
using FluentValidation;

namespace Discount.Application.Validators
{
    public class DeleteCouponValidator : AbstractValidator<DeleteCouponCommand>
    {
        public DeleteCouponValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id not provided.");
        }
    }
}
