using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginPlus.Models;
using LoginPlus.Factory;

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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
