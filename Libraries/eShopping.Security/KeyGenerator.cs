
using System.Security.Cryptography;

namespace eShopping.Security
{
    public static class KeyGenerator
    {
        public static string GenerateEncryptionKey()
        {
            return Convert.ToBase64String(GenerateRandomBytes(32)); // 256 bits
        }

        public static string GenerateEncryptionSecret()
        {
            return Convert.ToBase64String(GenerateRandomBytes(16)); // 128 bits
        }

        private static byte[] GenerateRandomBytes(int length)
        {
            byte[] randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
