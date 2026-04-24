
using System.Security.Cryptography;
using System.Text;

namespace D_A.Application.Utils
{
    public static class Cryptography
    {
        // IV fijo de 16 bytes (igual al ejemplo de la profesora)
        private static readonly byte[] IV =
        {
            33, 24, 31, 46, 75, 64, 97, 18,
            89, 10, 111, 132, 131, 144, 145, 250
        };

        /// <summary>
        /// Encripta el texto plano con AES-256 y devuelve los bytes cifrados.
        /// Se almacena directamente en Users.PasswordHash (byte[]).
        /// </summary>
        public static byte[] EncryptToBytes(string plainText, string secret)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] key = Encoding.UTF8.GetBytes(ComputeMd5Hash(secret[..32]));

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = IV;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        /// <summary>
        /// Compara la contraseña ingresada contra el hash almacenado en la BD.
        /// </summary>
        public static bool Verify(string plainText, byte[] storedHash, string secret)
        {
            byte[] hashOfInput = EncryptToBytes(plainText, secret);
            return hashOfInput.SequenceEqual(storedHash);
        }

        // MD5 del secret para derivar la key de 32 bytes (igual al ejemplo)
        private static string ComputeMd5Hash(string input)
        {
            var data = MD5.HashData(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var b in data) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}