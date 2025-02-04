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

        [HttpPost]
        public async Task<IActionResult> Register(t_User model)
        {
            if (ModelState.IsValid)
            {
                var result = await reg.Register(model);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}