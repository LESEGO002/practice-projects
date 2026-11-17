using Microsoft.AspNetCore.Mvc;
using proj_MVC.Models;
using System.Diagnostics;

namespace proj_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Ication method to register user
        public IActionResult CreateUser() 
        {//start of method 

            return View();//returns the view/page inside the view folder
        
        }//end of method

        [HttpPost]

        public IActionResult CreateUser(User userObj)
       {//start of method
            if (!ModelState.IsValid) 
            {
                ViewBag.User = userObj;
            }
          return View(userObj);
       }//end of method


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
