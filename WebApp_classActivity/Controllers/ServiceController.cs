using Microsoft.AspNetCore.Mvc;

namespace WebApp_classActivity.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Services()
        {
            return View();
        }
    }
}
