using Data.Repositories;
using eShopping.Exceptions;
using eShopping.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Users.Application.Extensions
{
    public static class UserExtensions
    {
        private const int ExpiryDurationDays = 90;

        /// <summary>
        /// Authenticates the user and returns a bearer token and other details.
        /// </summary>
        /// <param name="_unitOfWork">Unit of work object.</param>
        /// <param name="config">Config object.</param>
        /// <param name="userName">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="NotAuthorizedException"></exception>
        internal static async Task<UserLoginResponse> AuthenticateUser(this IUnitOfWorkCore _unitOfWork, DefaultConfig config, string userName, string password)
        {
            var user = await _unitOfWork.Repository<User>().Read()
                .Where(x => x.UserName == userName || x.UserEmail == userName)
                .Select(x => new
                {
                   UserId = x.Id,
                   UserName = x.UserName,
                   UserEmail = x.UserEmail,
                   PasswordSalt = x.PasswordSalt,
                   PasswordHash = x.PasswordHash,
                   Roles = _unitOfWork.Repository<UserRoleJoin>().Read()
                     .Where(ur => ur.UserId == x.Id)
                     .Join(_unitOfWork.Repository<Role>().Read(), ur => ur.RoleId, r => r.Id, (ur, r) => r)
                     .ToList()
                }).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException("Invalid UserName or Email.");
            }

            //Validate password
            if (user.PasswordHash != password.HashPassword(user.PasswordSalt))
            {
                throw new NotAuthorizedException("Invalid password.");
            }

            var result = new UserLoginResponse
            {
                UserId = user.UserId,
                UserEmail = user.UserEmail,
                UserName = user.UserName,
                Roles = user.Roles,
            };

            result.GenerateToken(config);

            return result;
        }

        internal static string GenerateToken(this UserLoginResponse user, DefaultConfig config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JWTSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
               new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.RoleName)));

            var token = new JwtSecurityToken(
                issuer: config.JWTIssuer,
                audience: config.JWTAudience,
                claims: claims,
                expires: DateTime.Now.AddMonths(config.PaswordExpiryMonths),
                signingCredentials: credentials);

            var bearer = new JwtSecurityTokenHandler().WriteToken(token);

            user.BearerToken = bearer;

            return bearer;
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
