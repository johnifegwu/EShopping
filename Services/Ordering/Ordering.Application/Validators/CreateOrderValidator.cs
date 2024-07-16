
using FluentValidation;
using Ordering.Application.Commands;
using Ordering.Application.Requests;
using Ordering.Core.Entities;

namespace Ordering.Application.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Payload).NotNull().WithMessage("Order can not be null.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username not provided")
                .Equal(x => x.Payload.UserName).WithMessage("You can not create order for another user.");
            
            RuleFor(x => x.Payload.OrderDetails).NotNull().WithMessage("Order details can not be null.");
            
            RuleFor(x => x.Payload.OrderDetails.Count).GreaterThan(0).WithMessage("Order details can not be empty.");
            
            // RuleForEach to validate each element in the OrderDetails list
            RuleForEach(o => o.Payload.OrderDetails).SetValidator(new OrderDetailValidator());
        }
    }

    public class OrderDetailValidator : AbstractValidator<CreateOrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(od => od.ProductId).NotEmpty().WithMessage("Product Id not provided.");

            RuleFor(od => od.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(od => od.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
