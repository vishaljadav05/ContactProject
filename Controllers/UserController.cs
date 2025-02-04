using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ContactProject.Models;
using ContactProject.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactProject.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserInterface reg;

        public UserController(IUserInterface reg)
        {
            this.reg = reg;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // [HttpPost]
        // public async Task<IActionResult> Register(t_User model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var result = await reg.Register(model);
        //         return RedirectToAction("Index","Home");
        //     }
        //     else
        //     {
        //         return View(model);
        //     }
        // }

        [HttpPost]
        public async Task<IActionResult> Register(t_User user)
        {
            if (ModelState.IsValid)
            {
                if (user.ProfilePicture != null && user.ProfilePicture.Length > 0)
                {
                    var fileName = user.c_Email + Path.GetExtension(user.ProfilePicture.FileName);
                    var filePath = Path.Combine("wwwroot/profile_images", fileName);
                    Directory.CreateDirectory(Path.Combine("wwwroot/profile_images"));
                    user.c_Image = fileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        user.ProfilePicture.CopyTo(stream);
                    }
                }
                Console.WriteLine("user.c_fname:"+user.c_UserName);
                var status=await reg.Register(user);
                if(status==1){
                    ViewData["message"]="User Registred";
                    // return RedirectToAction("Login");
                }
                else if(status==0){
                    ViewData["message"]="User Already Registred";

                }
                else{
                    ViewData["message"]="There was some error while Registration!!";
                }
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}