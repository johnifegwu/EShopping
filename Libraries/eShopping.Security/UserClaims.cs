using eShopping.Constants;
using System.Security.Claims;

namespace eShopping.Security
{
    public static class UserClaims
    {
        public static User GetUser(this ClaimsPrincipal principal)
        {
            return new User(principal);
        }
    }

    public class User
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? AdminRole {  get; set; }
        public string? CustomerRole {  get; set; }
        public List<string>? Roles { get; set; }

        public User() { }

        public User(ClaimsPrincipal principal)
        {
            this.UserName = principal.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            this.Email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            this.Roles = roles.Select(c => c.Value).ToList();
            this.AdminRole = Roles.FirstOrDefault(c => c.Equals(NameConstants.AdminRoleName, StringComparison.InvariantCultureIgnoreCase));
            this.CustomerRole = Roles.FirstOrDefault(c => c.Equals(NameConstants.CustomerRoleName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
