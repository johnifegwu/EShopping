using Data.Repositories;
using eShopping.Exceptions;
using eShopping.Models;
using eShopping.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        internal static async Task<UserLoginResponse> AuthenticateUser(this IUnitOfWorkCore _unitOfWork, DefaultConfig config, ILogger logger, string userName, string password)
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
                   PasswordExpiryDate = x.PasswordExpiryDate,
                   Roles = _unitOfWork.Repository<UserRoleJoin>().Read()
                     .Where(ur => ur.UserId == x.Id)
                     .Join(_unitOfWork.Repository<Role>().Read(), ur => ur.RoleId, r => r.Id, (ur, r) => r)
                     .ToList()
                }).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException("Invalid UserName or Email.");
            }

            //Check if Password has expired.
            if(user.PasswordExpiryDate < DateTime.UtcNow)
            {
                throw new ExpiredPasswordException();
            }

            //Validate password
            if (user.PasswordHash != password.HasStringValue(user.PasswordSalt))
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

            logger.LogInformation($"{user.UserName} attemped login at {DateTime.UtcNow}.");

            return result;
        }

        /// <summary>
        /// Gets all roles that belongs to the given user.
        /// </summary>
        /// <param name="_unitOfWork">IUnit of work object.</param>
        /// <param name="userId">User id.</param>
        /// <returns></returns>
        internal static async Task<List<Role>> GetUserRoles(this IUnitOfWorkCore _unitOfWork, int userId)
        {
            return await _unitOfWork.Repository<UserRoleJoin>().Read()
                     .Where(ur => ur.UserId == userId)
                     .Join(_unitOfWork.Repository<Role>().Read(), ur => ur.RoleId, r => r.Id, (ur, r) => r)
                     .ToListAsync();
        }

        /// <summary>
        /// Gets User data from the system.
        /// </summary>
        /// <param name="_unitOfWork">IUnit of work object.</param>
        /// <param name="UserName">User name.</param>
        /// <returns></returns>
        internal static async Task<User> GetUser(this IUnitOfWorkCore _unitOfWork, string UserName)
        {
            return await _unitOfWork.Repository<User>().Get().Where(x => x.UserName == UserName || x.UserEmail == UserName).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Gets user address from the system by Id.
        /// </summary>
        /// <param name="_unitOfWork">Unit of work object.</param>
        /// <param name="userId">User Id.</param>
        /// <param name="addressId">Address Id.</param>
        /// <returns></returns>
        internal static async Task<UserAddress> GetUserAddressById(this IUnitOfWorkCore _unitOfWork, int userId, int addressId)
        {
            return await _unitOfWork.Repository<UserAddress>().Get().Where(x => x.Id == addressId && x.UserId == userId).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Gets a list of users addresses from the system limited by page size.
        /// </summary>
        /// <param name="_unitOfWork">Unit of work object.</param>
        /// <param name="userId">User Id.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pagesize">Page size.</param>
        /// <returns></returns>
        internal static async Task<IList<UserAddress>> GetUserAddresses(this IUnitOfWorkCore _unitOfWork, int userId, int pageIndex, int pagesize)
        {
            if(pageIndex < 1 || pagesize < 1)
            {
                return new List<UserAddress>();
            }

            return await _unitOfWork.Repository<UserAddress>().Get().Where(x => x.UserId == userId).Skip((pageIndex - 1) * pagesize).Take(pagesize).ToListAsync();
        }


        /// <summary>
        /// Generates Access Token to be used as Bearer Token accross all eShopping Microservices.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="config">Config</param>
        /// <returns></returns>
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
                expires: DateTime.Now.AddMonths(config.BearerTokenExpiryMonths),
                signingCredentials: credentials);

            var bearer = new JwtSecurityTokenHandler().WriteToken(token);

            user.BearerToken = bearer;

            return bearer;
        }


        /// <summary>
        /// Creates a new User in the system.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <param name="userName">User name.</param>
        /// <param name="userEmail">User email.</param>
        /// <param name="password">Password.</param>
        /// <param name="PaswordExpiryMonths">Password expiry months.</param>
        /// <param name="CreatedBy">Created by.</param>
        /// <returns></returns>
        public static User CreateUser(this User user, string userName, string userEmail, string password, int PaswordExpiryMonths, string CreatedBy)
        {
            var salt = password.GenerateSalt();
            var hashedPassword = password.HasStringValue(salt);

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
