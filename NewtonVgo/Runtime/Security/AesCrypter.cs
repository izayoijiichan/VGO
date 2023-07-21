// ----------------------------------------------------------------------
// @Namespace : NewtonVgo.Security.Cryptography
// @Class     : AesCrypter
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo.Security.Cryptography
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Advanced Encryption Standard Crypter
    /// </summary>
    public class AesCrypter
    {
        #region Properties

        /// <summary>The block cipher mode to use for encryption.</summary>
        public CipherMode CipherMode { get; set; } = CipherMode.CBC;

        /// <summary>The type of padding to apply when the message data block is shorter than the full number of bytes needed for a cryptographic operation.</summary>
        public PaddingMode PaddingMode { get; set; } = PaddingMode.PKCS7;

        #endregion

        #region Public Methods

        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="key">The key to be used for the algorithm.</param>
        /// <param name="iv">The initialization vector to be used for the algorithm.</param>
        /// <returns></returns>
        public byte[] Encrypt(in byte[] src, in byte[] key, in byte[] iv)
        {
            using Aes aes = Aes.Create();

            aes.Mode = CipherMode;
            aes.Padding = PaddingMode;

            aes.Key = key;
            aes.IV = iv;

            using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            return encryptor.TransformFinalBlock(src, 0, src.Length);
        }

        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="key">The key to be used for the algorithm.</param>
        /// <param name="blockSize"></param>
        /// <param name="iv">The initialization vector.</param>
        /// <param name="ivString">The initialization vector string (Base64).</param>
        /// <returns></returns>
        public byte[] Encrypt(in byte[] src, in byte[] key, in int blockSize, out byte[] iv, out string ivString)
        {
            using Aes aes = Aes.Create();

            aes.Mode = CipherMode;
            aes.Padding = PaddingMode;

            aes.BlockSize = blockSize;

            aes.Key = key;
            aes.GenerateIV();

            iv = aes.IV;

            ivString = Convert.ToBase64String(iv);

            using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            return encryptor.TransformFinalBlock(src, 0, src.Length);
        }

        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="keySize"></param>
        /// <param name="blockSize"></param>
        /// <param name="key">The key.</param>
        /// <param name="keyString">The key string (Base64).</param>
        /// <param name="iv">The initialization vector.</param>
        /// <param name="ivString">The initialization vector string (Base64).</param>
        /// <returns></returns>
        public byte[] Encrypt(in byte[] src, in int keySize, in int blockSize, out byte[] key, out string keyString, out byte[] iv, out string ivString)
        {
            using Aes aes = Aes.Create();

            aes.Mode = CipherMode;
            aes.Padding = PaddingMode;

            aes.KeySize = keySize;
            aes.BlockSize = blockSize;

            aes.GenerateKey();
            aes.GenerateIV();

            key = aes.Key;
            iv = aes.IV;

            keyString = Convert.ToBase64String(key);
            ivString = Convert.ToBase64String(iv);

            using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            return encryptor.TransformFinalBlock(src, 0, src.Length);
        }

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="key">The key string (Base64) to be used for the algorithm.</param>
        /// <param name="iv">The initialization vector string (Base64) to be used for the algorithm..</param>
        /// <returns></returns>
        public byte[] Decrypt(in byte[] src, in string key, in string iv)
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            return Decrypt(src, keyBytes, ivBytes);
        }

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="key">The key to be used for the algorithm.</param>
        /// <param name="iv">The initialization vector to be used for the algorithm..</param>
        /// <returns></returns>
        public byte[] Decrypt(in byte[] src, in byte[] key, in byte[] iv)
        {
            using Aes aes = Aes.Create();

            aes.Key = key;
            aes.IV = iv;

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            return decryptor.TransformFinalBlock(src, 0, src.Length);
        }

        #endregion

        #region Public Methods (Helper)

        /// <summary>
        /// Generate a random key.
        /// </summary>
        /// <param name="keySize"></param>
        public byte[] GenerateRandomKey(in int keySize)
        {
            int keyByteSize = keySize / 8;

            byte[] bytes = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            StringBuilder sb = new StringBuilder(32);

            foreach (byte b in bytes)
            {
                sb.Append(Convert.ToChar(b));
            }

#if UNITY_2021_2_OR_NEWER  // CSHARP_8_OR_LATER
            using var deriveBytes = new Rfc2898DeriveBytes(password: sb.ToString(), saltSize: 16, iterations: 1000, hashAlgorithm: HashAlgorithmName.SHA1);
#else
            using var deriveBytes = new Rfc2898DeriveBytes(password: sb.ToString(), saltSize: 16, iterations: 1000);
#endif
            return deriveBytes.GetBytes(keyByteSize);
        }

        #endregion
    }
}
