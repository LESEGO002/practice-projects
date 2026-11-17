using Microsoft.AspNetCore.Mvc;

namespace WebApp_Example1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Welcome to the Home Page!";
            return View();
        }
    }
}
