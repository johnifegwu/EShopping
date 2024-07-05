
using Catalog.Application.Commands.Products;
using FluentValidation;

namespace Catalog.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Payload).NotNull().WithMessage("Product request object can not be null");
            RuleFor(x => x.Payload.Name).NotEmpty().WithMessage("Name not provided.");
        }
    }
}
