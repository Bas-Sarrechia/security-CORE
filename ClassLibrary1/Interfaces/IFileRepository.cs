using System;
using System.Collections.Generic;
using System.Text;
using Security.Domain;

namespace Security.Data.Interfaces
{
    public interface IFileRepository
    {
        string[] GetDecypted(int fromUserid, int fileId);
        bool EncyptAndStore(int toUserId, int fromUserId, string base64Data, string fileName);
        IEnumerable<FilePackage> GetAll();
    }
}
