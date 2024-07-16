
using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class UpdateOrderValidators : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidators()
        {
            RuleFor(x => x.Payload).NotNull().WithMessage("Order can not be null.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username not provided")
                .Equal(x => x.Payload.UserName).WithMessage("You can not create order for another user.");
        }
    }
}
