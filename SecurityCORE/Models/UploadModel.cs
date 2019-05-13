using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Security.Domain;

namespace SecurityCORE.Models
{
    public class UploadModel
    {
        public int ToUserId { get; set; }
        public int FromUserId { get; set; }
        public string Base64Data { get; set; }
        public string FileName { get; set; }
    }
}
