using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Data.Interfaces
{
    public interface IKeysRepository
    {
        byte[] GetPrivateKey(int userId);
        byte[] GetPublicKey(int userId);
    }
}
