
using eShopping.Constants;
using eShopping.Models;
using FluentValidation;
using Microsoft.Extensions.Options;
using Users.Application.Commands;

namespace Users.Application.Validators
{
    public class RemoveUserFromAdminRoleValidator : AbstractValidator<RemoveUserFromAdminRoleCommand>
    {
        private readonly DefaultConfig _config;
        public RemoveUserFromAdminRoleValidator(IOptions<DefaultConfig> config)
        {
            _config = config.Value;
            RuleFor(x => (x.CurrentUser == null || x.CurrentUser.IsInRole(NameConstants.AdminRoleName) == false || x.CurrentUser.UserName != _config.DefaultUserName)).Equal(true).WithMessage("Un-Authorized, access denied.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UseName must not be empty.");
        }
    }
}
