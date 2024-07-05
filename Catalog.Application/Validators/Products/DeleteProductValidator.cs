
using Catalog.Application.Commands.Products;
using FluentValidation;

namespace Catalog.Application.Validators.Products
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Id not provided.");
        }
    }
}
