
using Catalog.Application.Commands.Products;
using FluentValidation;

namespace Catalog.Application.Validators.Products
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidation()
        {
            RuleFor(x => x.Payload).NotNull().WithMessage("Product request object can not be null");
            RuleFor(x => x.Payload.Id).NotEmpty().WithMessage("Id not provided.");
            RuleFor(x => x.Payload.Name).NotEmpty().WithMessage("Name not provided.");
        }
    }
}
