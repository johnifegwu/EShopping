using eShopping.Constants;
using System.Security.Claims;

namespace eShopping.Security
{
    public static class ClaimsExtensions
    {
        public static UserClaims GetUserClaims(this ClaimsPrincipal principal)
        {
            return new UserClaims(principal);
        }
    }

    public class UserClaims
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();

        public UserClaims() { }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        public UserClaims(ClaimsPrincipal principal)
        {
            this.UserName = principal.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            this.Email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            this.Roles = roles.Select(c => c.Value).ToList();
        }
    }
}
