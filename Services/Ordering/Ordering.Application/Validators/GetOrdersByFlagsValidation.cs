
using FluentValidation;
using Ordering.Application.Queries;

namespace Ordering.Application.Validators
{
    public class GetOrdersByFlagsValidation : AbstractValidator<GetOrdersByFlagsQuery>
    {
        public GetOrdersByFlagsValidation()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("PageIndex can not be less than 1");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100).WithMessage("Page size must be between 1 to 100");
        }
      
    }
}
