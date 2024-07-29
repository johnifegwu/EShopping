
using eShopping.Models;
using System.Security.Cryptography;
using System.Text;

namespace eShopping.Security
{
    public static class Encryptors
    {
        public static string GenerateSalt(this string? str)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            str = Convert.ToBase64String(saltBytes);
            return str;
        }

        public static string HasStringValue(this string stringValue, string salt)
        {
            var sha256 = SHA256.Create();
            var saltedPassword = stringValue + salt;
            var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
            var hashedPasswordBytes = sha256.ComputeHash(saltedPasswordBytes);
            stringValue = Convert.ToBase64String(hashedPasswordBytes);

            return stringValue;
        }

        public static async Task<string> EncryptString(this string strValue, DefaultConfig _config)
        {
            if (string.IsNullOrEmpty(strValue))
                throw new ArgumentNullException(nameof(strValue));
            if (string.IsNullOrEmpty(_config.EncryptionKey))
                throw new ArgumentNullException(nameof(_config.EncryptionKey));
            if (string.IsNullOrEmpty(_config.EncryptionSecret))
                throw new ArgumentNullException(nameof(_config.EncryptionSecret));

            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(_config.EncryptionKey);
                aes.IV = Convert.FromBase64String(_config.EncryptionSecret);

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        await sw.WriteAsync(strValue);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static async Task<string> DecryptString(this string encryptedValue, DefaultConfig _config)
        {
            if (string.IsNullOrEmpty(encryptedValue))
                throw new ArgumentNullException(nameof(encryptedValue));
            if (string.IsNullOrEmpty(_config.EncryptionKey))
                throw new ArgumentNullException(nameof(_config.EncryptionKey));
            if (string.IsNullOrEmpty(_config.EncryptionSecret))
                throw new ArgumentNullException(nameof(_config.EncryptionSecret));

            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(_config.EncryptionKey);
                aes.IV = Convert.FromBase64String(_config.EncryptionSecret);

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(Convert.FromBase64String(encryptedValue)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return await sr.ReadToEndAsync();
                }
            }
        }
    
    }
}
