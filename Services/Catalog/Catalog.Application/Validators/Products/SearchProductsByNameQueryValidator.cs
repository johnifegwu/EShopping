using Catalog.Application.Queries.Products;
using FluentValidation;

namespace Catalog.Application.Validators.Products
{
    public class SearchProductsByNameQueryValidator : AbstractValidator<SearchProductsByNameQuery>
    {
        public SearchProductsByNameQueryValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("Pageindex must be greater than 1");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100).WithMessage("Pagesize must be between 1 and 100");
        }
    }
}
