using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Security.Domain.Crypto
{
    public class RSATool
    {
        internal static Keys GenerateKeys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096) {PersistKeyInCsp = true};

            return new Keys()
            {
                PublicKey = rsa.ExportCspBlob(false),
                PrivateKey = rsa.ExportCspBlob(true),
            };
        }

        internal static byte[] Encrypt(byte[] data, byte[] publicKey)
        {
            return CreateRsaService(publicKey).Encrypt(data, true);
        }

        internal static byte[] Decrypt(byte[] data, byte[] privateKey)
        {
            return CreateRsaService(privateKey).Decrypt(data, true);
        }

        private static RSACryptoServiceProvider CreateRsaService(byte[] privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096) {PersistKeyInCsp = false};

            rsa.ImportCspBlob(privateKey);

            return rsa;
        }

        internal static byte[] GenerateSignature(byte[] dataToSign, byte[] privateKey)
        {
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(CreateRsaService(privateKey));
            formatter.SetHashAlgorithm("SHA256");
            byte[] signedData = formatter.CreateSignature(dataToSign);
            return signedData;
        }
    }
}
