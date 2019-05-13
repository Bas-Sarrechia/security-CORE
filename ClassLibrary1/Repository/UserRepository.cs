using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Security.Data.Interfaces;
using Security.Domain;
using Security.Domain.Crypto;

namespace Security.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly SecurityContext _context;

        public UserRepository(SecurityContext context)
        {
            this._context = context;
        }

        public User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(user => user.Username == username);
        }

        public int GetId(string user)
        {
            User dbUser = _context.Users.FirstOrDefault(dbuser => dbuser.Username.Equals(user));
            if (dbUser != null)
            {
                return dbUser.Id;
            }

            return -1;
        }

        public bool AddUser(User user)
        {
            if (user != null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool ValidateUsername(string username)
        {
            return _context.Users.FirstOrDefault(user => user.Username.Equals(username)) != null;
        }

        public bool ValidatePassword(string password)
        {
            return _context.Users.FirstOrDefault(user => user.Password.Equals(Convert.ToBase64String( SaltAndPepper.SaltMyPassword(password,user.Salt)))) != null;
        }

        internal string GetSalt(int id)
        {
            User dbUser = _context.Users.FirstOrDefault(user => user.Id == id);
            return dbUser?.Salt;
        }

        public bool Contains(string username)
        {
            return _context.Users.FirstOrDefault(dbuser => dbuser.Username.Equals(username)) != null;
        }

        public IEnumerable<User> getAll()
        {
            return _context.Users.ToList();
        }
    }
}