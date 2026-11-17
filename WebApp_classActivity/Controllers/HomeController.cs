using Microsoft.AspNetCore.Mvc;

namespace WebApp_classActivity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
