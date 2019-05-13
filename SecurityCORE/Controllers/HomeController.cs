using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.Data.Interfaces;
using Security.Domain;
using SecurityCORE.Models;
using System.Web;

namespace SecurityCORE.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;

        public HomeController(IUserRepository userRepository, IFileRepository fileRepository)
        {
            _userRepository = userRepository;
            _fileRepository = fileRepository;
        }


        [HttpPost("/upload/")]
        public IActionResult Index(UploadModel model)
        {
            return View();
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexModel
            {
                Users = _userRepository.getAll(),
                Files = _fileRepository.GetAll()
                
            };
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.ValidateUsername(model.UserName))
                {   
                    if (_userRepository.ValidatePassword(model.Password))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("Invalid password", "password doesn't match");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("Invalid username", "The username entered does not exist");
                    return View();
                }
            }

            return View();
        }


        [HttpGet]
        public IActionResult CreateNewUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.Contains(model.UserName))
                {
                    ModelState.AddModelError("duplicate user", "username already exists!");
                    return View();
                }

                _userRepository.AddUser(new User(model.Password, model.UserName.ToLower()));
                return RedirectToAction(nameof(Login));
            }

            return View();
        }
    }
}