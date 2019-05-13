using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Security.Data.Interfaces;
using Security.Domain;
using Security.Domain.Crypto;

namespace Security.Data.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly SecurityContext _context;

        public FileRepository(SecurityContext context)
        {
            _context = context;
        }

        public bool EncyptAndStore(int toUserId, int fromUserId, string base64Data, string fileName)
        {
            HybridGenerator package = new HybridGenerator();
            User toUser = _context.Users.Include(user => user.UserKeys).FirstOrDefault(user => user.Id == toUserId);
            User fromUser = _context.Users.Include(user => user.UserKeys).FirstOrDefault(user => user.Id == fromUserId);
            byte[] privateKey = fromUser.UserKeys.PrivateKey;
            byte[] publicKey = toUser.UserKeys.PublicKey;

            if (toUser == null || privateKey == null) 
            {
                return false;
            }

            byte[] data = Convert.FromBase64String(base64Data.Split(',')[1]);

            _context.Files.Add(package.EncryptPackage(data, fileName,toUser,fromUser,privateKey, publicKey));
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<FilePackage> GetAll()
        {
            return _context.Files;
        }

        public string[] GetDecypted(int fromUser, int fileId)
        {
            var foundFile = _context.Files.Include(file => file.FromUser).Include(file => file.ToUser).FirstOrDefault(file => file.Id == fileId);
            var foundUser = _context.Users.Include(user => user.UserKeys).FirstOrDefault(user => user.Id == fromUser);
            HybridGenerator decrypted = new HybridGenerator();

            var result = decrypted.DecryptPackage(foundFile, foundUser.UserKeys.PrivateKey);

            return new []{ result.FileName, Convert.ToBase64String(result.PackageData)};
        }
    }
}