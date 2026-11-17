using Microsoft.AspNetCore.Mvc;

namespace WebApp_classActivity.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact_us()
        {
            return View();
        }
    }
}
