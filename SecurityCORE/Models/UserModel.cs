using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityCORE.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Username  is required")]
        [MinLength(3, ErrorMessage = "A username contains atleast 3 letters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter a password!")]
        [MinLength(5, ErrorMessage = "A password contains 5 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
