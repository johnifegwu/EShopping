using Data.Repositories;
using Discount.Application.Commands;
using FluentValidation;
using Discount.Application.Extensions;

namespace Discount.Application.Validators
{
    public class UpdateCouponValidator : AbstractValidator<UpdateCouponCommand>
    {
        public UpdateCouponValidator()
        {
            RuleFor(x => x.Payload).NotNull().WithMessage("Payload can not be null.");
            RuleFor(x => x.Payload.Amount).GreaterThanOrEqualTo(0).WithMessage("Amount can not be less than zero.");
            RuleFor(x => x.Payload.ProductId).NotEmpty().WithMessage("Product Id not provided.");
            RuleFor(x => x.Payload.Id).NotEmpty().WithMessage("Id not provided.");
            RuleFor(x => x.Payload.ProductName).NotEmpty().WithMessage("Product name not provided.");   
        }
    }
}
