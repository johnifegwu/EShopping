
using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class DeleteOrderValidation : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderValidation()
        {
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Order Id must be greater than zero");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name not provided.");
        }
    }
}
