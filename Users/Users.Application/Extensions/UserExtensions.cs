using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Core.Entities;
using Users.Core.Models;

namespace Users.Application.Extensions
{
    public static class UserExtensions
    {
        private const int ExpiryDurationDays = 90;

        public static string GenerateToken(this User user, IOptions<DefaultConfig> config)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.Value.SecretKey);

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(ClaimTypes.Email, user.UserEmail)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.RoleName));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(ExpiryDurationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static User CreateUser(this User user, string userName, string userEmail, string password, int PaswordExpiryMonths, string CreatedBy)
        {
            var salt = password.GenerateSalt();
            var hashedPassword = password.HashPassword(salt);

            user.UserName = userName;
            user.UserEmail = userEmail;
            user.PasswordSalt = salt;
            user.PasswordHash = hashedPassword;
            user.PasswordExpiryDate = DateTime.UtcNow.AddMonths(PaswordExpiryMonths);
            user.CreatedBy = CreatedBy;
            user.CreatedDate = DateTime.UtcNow;

            return user;
        }
    }
}
