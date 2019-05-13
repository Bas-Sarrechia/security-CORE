using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Security.Cryptography;

namespace Security.Domain.Crypto
{
    public class SaltAndPepper
    {

       public static byte[] SaltMyPassword(string password, string salt)
       {
           Rfc2898DeriveBytes salter = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt),5000);
           return salter.GetBytes(32);
       }

       public static byte[] GenerateSalt()
       {
           byte[] saltBytes = new byte[32];
           new RNGCryptoServiceProvider().GetBytes(saltBytes);
           return saltBytes;
       }

    }
}
