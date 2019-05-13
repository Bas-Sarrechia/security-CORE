using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Security.Domain.Crypto
{
    class AesTool
    {
        internal static byte[] GenerateNumber(int length)
        {

            byte[] randomBytes = new byte[length];

            new RNGCryptoServiceProvider().GetBytes(randomBytes);

            return randomBytes;
        }

        internal static byte[] AesEncrypt(byte[] data, byte[] iv, byte[] key)
        {
            MemoryStream encryptingStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(encryptingStream, CreateAesService(iv, key).CreateEncryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return encryptingStream.ToArray();
        }

        internal static byte[] AesDecrypt(byte[] data, byte[] iv, byte[] key)
        {
            MemoryStream encryptingStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(encryptingStream, CreateAesService(iv, key).CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);

            return encryptingStream.ToArray();
        }

        private static AesCryptoServiceProvider CreateAesService(byte[] iv, byte[] key)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider {IV = iv, Key = key};

            return aes;
        }
    }

}

