using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security.Data.Interfaces;
using Security.Domain;

namespace Security.Data.Repository
{
    public class KeysRepository : IKeysRepository
    {
        private readonly SecurityContext _context;

        public KeysRepository(SecurityContext context)
        {
            _context = context;
        }

        public byte[] GetPrivateKey(int userId)
        {
            return _context.Keys.FirstOrDefault(key => key.User.Id == userId)?.PrivateKey;
        }

        public byte[] GetPublicKey(int userId)
        {
            return _context.Keys.FirstOrDefault(key => key.User.Id == userId)?.PublicKey;
        }
    }
}