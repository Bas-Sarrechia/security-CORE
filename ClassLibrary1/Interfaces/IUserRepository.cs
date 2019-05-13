using System;
using System.Collections.Generic;
using System.Text;
using Security.Domain;

namespace Security.Data.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string username);
        int GetId(string user);
        bool AddUser(User user);
        bool ValidateUsername(string username);
        bool ValidatePassword(string password);
        bool Contains(string username);

        IEnumerable<User> getAll();
    }
}
