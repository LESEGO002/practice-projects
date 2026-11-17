using Microsoft.AspNetCore.Mvc;

namespace WebApp_Example1.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Accounts()
        {
            return View();
        }
    }
}
