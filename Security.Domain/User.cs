using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Security.Domain.Crypto;

namespace Security.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public  Keys UserKeys { get; set; }
        [ForeignKey("Keys")]
        public int UserKeysId { get; set; }
        public User()
        {

        }

        public User(string passwordUnsalted, string username)
        {
            this.Username = username;
            Salt = Convert.ToBase64String(SaltAndPepper.GenerateSalt());
            Password = Convert.ToBase64String(SaltAndPepper.SaltMyPassword(passwordUnsalted, Salt));
            UserKeys = RSATool.GenerateKeys();
        }
    }
}
