using Discussion_Forum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Discussion_Forum.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}