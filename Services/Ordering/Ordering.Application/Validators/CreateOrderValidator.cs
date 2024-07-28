
using FluentValidation;
using Ordering.Application.Commands;
using Ordering.Application.Requests;

namespace Ordering.Application.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Payload).NotNull().WithMessage("Order can not be null.");

            RuleFor(x => x.CurrentUser.UserName)
            .NotEmpty().WithMessage("UserName is required.");

            RuleFor(x => x.CurrentUser.UserName == x.Payload.UserName).NotEqual(false)
                .WithMessage("You can not creat order for another customer.");

            RuleFor(x => x.Payload.OrderGuid)
                .NotEmpty().WithMessage("OrderGuid is required.");

            RuleFor(x => x.Payload.TotalPrice)
                .GreaterThan(0).WithMessage("TotalPrice must be greater than zero.");

            RuleFor(x => x.Payload.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be a 3-letter ISO code.");

            RuleFor(x => x.Payload.FirstName)
                .NotEmpty().WithMessage("FirstName is required.");

            RuleFor(x => x.Payload.LastName)
                .NotEmpty().WithMessage("LastName is required.");

            RuleFor(x => x.Payload.EmailAddress)
                .NotEmpty().WithMessage("EmailAddress is required.")
                .EmailAddress().WithMessage("EmailAddress must be a valid email address.");

            RuleFor(x => x.Payload.AddressLine1)
                .NotEmpty().WithMessage("AddressLine1 is required.");

            RuleFor(x => x.Payload.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.Payload.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.");

            RuleFor(x => x.Payload.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleFor(x => x.Payload.CardName)
                .NotEmpty().WithMessage("CardName is required.");

            RuleFor(x => x.Payload.CardNumber)
                .NotEmpty().WithMessage("CardNumber is required.")
                .CreditCard().WithMessage("CardNumber must be a valid credit card number.");

            RuleFor(x => x.Payload.CardType)
                .NotEmpty().WithMessage("CardType is required.");

            RuleFor(x => x.Payload.Expiration)
                .NotEmpty().WithMessage("Expiration is required.");

            RuleFor(x => x.Payload.CVV)
                .NotEmpty().WithMessage("CVV is required.")
                .Length(3, 4).WithMessage("CVV must be 3 or 4 digits.");

            RuleForEach(x => x.Payload.OrderDetails)
                .SetValidator(new OrderDetailValidator());
        }
    }

    public class OrderDetailValidator : AbstractValidator<CreateOrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId must be greater than zero.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("ProductName is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}
