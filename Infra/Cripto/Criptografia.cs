using System.Security.Cryptography;
using System.Text;

namespace Infra
{
    public static class Criptografia
    {

        public static string Encrypt(string token, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] plainBytes = Encoding.UTF8.GetBytes(token);

            using (var aes = new AesCryptoServiceProvider())
            {
                var keym = GenerateRandomKey();
                aes.Key = keyBytes;
                aes.GenerateIV();
                byte[] encryptedBytes = aes.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                byte[] combinedBytes = new byte[aes.IV.Length + encryptedBytes.Length];
                Buffer.BlockCopy(aes.IV, 0, combinedBytes, 0, aes.IV.Length);
                Buffer.BlockCopy(encryptedBytes, 0, combinedBytes, aes.IV.Length, encryptedBytes.Length);

                return Convert.ToBase64String(combinedBytes);
            }
        }

        public static string Decrypt(string encryptedToken, string key)
        {
            byte[] combinedBytes = Convert.FromBase64String(encryptedToken);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] iv = new byte[16];
            byte[] encryptedBytes = new byte[combinedBytes.Length - iv.Length];
            Buffer.BlockCopy(combinedBytes, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(combinedBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Key = keyBytes;
                aes.IV = iv;
                byte[] decryptedBytes = aes.CreateDecryptor().TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        private static string GenerateRandomKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[32];
                rng.GetBytes(keyBytes);
                return Encoding.UTF8.GetString(keyBytes);
            }
        }

    }
}
