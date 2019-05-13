using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Security.Data.Interfaces;
using SecurityCORE.Models;

namespace SecurityCORE.Controllers
{
    public class UploadFilesController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUserRepository _userRepository;

        public UploadFilesController(IFileRepository fileRepository, IUserRepository userRepository)
        {
            _fileRepository = fileRepository;
            _userRepository = userRepository;
        }


        [HttpPost("UploadFiles")]
        public ActionResult Post([FromBody] UploadModel model)
        {
            _fileRepository.EncyptAndStore(model.ToUserId,model.FromUserId,model.Base64Data, model.FileName);
            return Ok();
        }

        [HttpGet("Download/myUser/{userId}/file/{fileId}")]
        public ActionResult DownloadFile(int userId, int fileId)
        {
            try
            {
                var result = _fileRepository.GetDecypted(userId, fileId);

                if (result != null)
                {
                    return File(Convert.FromBase64String(result[1]), System.Net.Mime.MediaTypeNames.Application.Octet,
                        result[0]);
                }
            }
            catch
            {
                new HomeController(_userRepository,_fileRepository).Index();
            }

            return BadRequest();
        }
    }
}