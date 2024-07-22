
using System.Security.Cryptography;
using System.Text;

namespace Users.Application.Extensions
{
    public static class StringExtention
    {

        public static string GenerateSalt(this string? str)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(this string password, string salt)
        {
            var sha256 = SHA256.Create();
            var saltedPassword = password + salt;
            var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
            var hashedPasswordBytes = sha256.ComputeHash(saltedPasswordBytes);
            password =  Convert.ToBase64String(hashedPasswordBytes);

            return password;
        }
    }
}
