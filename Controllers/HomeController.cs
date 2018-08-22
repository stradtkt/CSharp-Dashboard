using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginPlus.Models;
using LoginPlus.Factory;
using Microsoft.AspNetCore.Identity;
using Login.Models;

namespace LoginPlus.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserFactory _userFactory;
 
        public HomeController(UserFactory uFactory)
        {
            _userFactory = uFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(string Email, string PasswordToCheck)
        {
            if(ModelState.IsValid)
            {
                var user = _userFactory.GetUserByEmail(Email);
                if(user != null && PasswordToCheck != null)
                {
                    var Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(user, user.password, PasswordToCheck))
                    {
                        return View("Index");
                    }
                }
            }
            return View("Login");
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(User user)
        {
            if(ModelState.IsValid)
            {
                if(!_userFactory.EmailIsUnique(user.email))
                {
                    ModelState.AddModelError("email", "This email already exists");
                    return View("Register");
                }
                else
                {
                    _userFactory.RegisterUser(user);
                    return RedirectToAction("Login");
                }
            }
            return View("Register");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
