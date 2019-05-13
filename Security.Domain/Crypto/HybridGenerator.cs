using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Security.Domain.Crypto
{
    public class HybridGenerator
    {
        public FilePackage EncryptPackage(byte[] data, string filename, User fromUser, User toUser, byte[] privateKeyFromUser, byte[] publicKeyToUser)
        {
            byte[] aesKey = AesTool.GenerateNumber(32);
            byte[] iv = AesTool.GenerateNumber(16);
            byte[] encryptedData = AesTool.AesEncrypt(data, iv, aesKey);
            byte[] hmac = new HMACSHA256(aesKey).ComputeHash(encryptedData);

            return new FilePackage()
            {
                FromUser = fromUser,
                ToUser = toUser,
                FileName = filename,
                PackageData = encryptedData,
                Iv = iv,
                EncryptedSessionKey = RSATool.Encrypt(aesKey, publicKeyToUser),
                Hmac = hmac,
                Signature = RSATool.GenerateSignature(hmac, privateKeyFromUser)
            };
        }

        public FilePackage DecryptPackage(FilePackage package, byte[] privateKey)
        {
            byte[] decryptedSessionKey = RSATool.Decrypt(package.EncryptedSessionKey, privateKey);

            if (!CompareHmacSecure(package.Hmac, new HMACSHA256(decryptedSessionKey).ComputeHash(package.PackageData)))
            {
                throw new CryptographicException("HMAC doesn't match for the encrypted package for decryption");
            }

            package.PackageData = AesTool.AesDecrypt(package.PackageData, package.Iv, decryptedSessionKey);

            return package;
        }

        internal bool CompareHmacSecure(byte[] data1, byte[] data2)
        {
            bool result = data1.Length == data2.Length;

            for (int i = 0; i < data1.Length; i++)
            {
                if (result && data1[i] != data2[i])
                {
                    result = false;
                }
            }

            return result;
        }
    }
}