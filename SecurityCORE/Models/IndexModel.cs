using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Security.Domain;

namespace SecurityCORE.Models
{
    public class IndexModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<FilePackage> Files { get; set; }
    }
}
