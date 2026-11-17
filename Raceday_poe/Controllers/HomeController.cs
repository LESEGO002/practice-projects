using Microsoft.AspNetCore.Mvc;
using Raceday_poe.Models;
using System.Diagnostics;
using Raceday_poe.Servicers;

namespace Raceday_poe.Controllers
{
    public class HomeController : Controller
    {
        // Api_sercices;

        //start of field
        private readonly Api_sercices _user_api_service;
        //end of field

        //start of constructor
        public HomeController(Api_sercices user_api_service)
        {
            _user_api_service = user_api_service;
        }
        //end of constructor

        //start of method
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //end of method

        //start of method
        public IActionResult RegisterUser()
        {//start of method

            return View(new RegisterModel());

        }//end of method

        //start of method
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterModel regObj)
        {//start of method

            if (!ModelState.IsValid)
            {
                return View(regObj);
            }

            string message = await _user_api_service.UserRegistration(regObj);

            if (message.Contains("successfully", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Login");
            }
            ViewBag.Message = message;
            return View(regObj);

        }//end of method

        //start of method
        public IActionResult Login()
        {//start of method

            return View(new LoginModel());

        }//end of method

        //start of method
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel LogObj)
        {//start of method

            if (!ModelState.IsValid)
            {
                return View(LogObj);
            }

            string message = await _user_api_service.UserLogin(LogObj);

            if (message.Contains("Logged in successfully...", StringComparison.OrdinalIgnoreCase))
            {
                HttpContext.Session.SetString("Email", LogObj.Email);

                string role = await _user_api_service.GetUserRole(LogObj.Email);

                if (role.Equals("Organizer", StringComparison.OrdinalIgnoreCase) || role.Equals("Organizer", StringComparison.OrdinalIgnoreCase))
                {
                    HttpContext.Session.SetString("Role", "Organizer");
                    return RedirectToAction("Dashboard", "Organizer");
                }
                else
                {
                    HttpContext.Session.SetString("Role", "Participant");
                    return RedirectToAction("ParticipantHome", "Participant");
                }
            }

            ViewBag.Message = message;
            return View(LogObj);

        }//end of method


        //start of method
        public IActionResult Logout()
        {//start of method

            HttpContext.Session.Clear();
            return RedirectToAction("Index");

        }//end of method

        //start of method
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //end of method
    }
}
