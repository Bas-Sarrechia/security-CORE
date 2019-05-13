using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Domain
{
    public class FilePackage
    {
        public int Id { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
        public string FileName { get; set; }
        public byte[] PackageData { get; set; }
        public byte[] Iv { get; set; }
        public byte[] EncryptedSessionKey { get; set; }
        public byte[] Hmac { get; set; }
        public byte[] Signature { get; set; }
    }
}



