using Microsoft.AspNetCore.Mvc;

namespace WebApp_classActivity.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About_us()
        {
            return View();
        }
    }
}
