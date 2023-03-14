using System;
using Microsoft.AspNetCore.Mvc;

namespace CarTest.Controllers
{
    public partial class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
