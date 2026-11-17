using Microsoft.AspNetCore.Mvc;
using Raceday_poe.Servicers;
using Raceday_poe.Models;

namespace Raceday_poe.Controllers
{
    public class OrganizerController : Controller
    {
        //start of field
        private readonly Api_sercices _context;
        //end of field

        //start of constructor
        public OrganizerController(Api_sercices context)
        {
            _context = context;
        }
        //end of constructor

        //start of method
        public async Task<IActionResult> Dashboard()
        {//start of method

           
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }
            if (!HttpContext.Session.GetString("Role")!.Equals("Organizer", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("ParticipantHome", "Participant");
            }

            List<EventModel> events = await _context.GetAllEvents();
            List<EnrollmentModel> enrollments = await _context.GetAllEnrollments();

            ViewBag.TotalEvents = events.Count;
            ViewBag.TotalEnrollments = enrollments.Count;
            ViewBag.UpcomingEvents = events.Where(e => e.EventDate >= DateTime.Today).OrderBy(e => e.EventDate).Take(5).ToList();

            return View(events);

        }//end of method

        //start of method
        public async Task<IActionResult> Enrollments()
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            List<EnrollmentModel> enrollments = await _context.GetAllEnrollments();
            return View(enrollments);

        }//end of method

        //start of method
        public IActionResult CreateCategory()
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            return View(new CategoryModel());

        }//end of method

        //start of method
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryModel cat)
        {//start of method

            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            cat.organizerEmail = HttpContext.Session.GetString("Email") ?? "";
            string message = await _context.CreateCategory(cat);

            if (message.Contains("successfully", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Categories");
            }
            ViewBag.Message = message;
            return View(cat);

        }//end of method

        //start of method
        public async Task<IActionResult> Categories()
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            List<CategoryModel> categories = await _context.GetAllCategories();
            return View(categories);

        }//end of method

        //start of method
        public async Task<IActionResult> Results()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                return RedirectToAction("Login", "Home");

            List<ResultModel> results = await _context.GetAllResults();
            return View(results);
        }

        //start of method
        public IActionResult CaptureResult()
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            return View(new ResultModel());

        }//end of method

        //start of method
        [HttpPost]
        public async Task<IActionResult> CaptureResult(ResultModel result)
        {//start of method

            if (!ModelState.IsValid)
            {
                return View(result);
            }
            result.organizerEmail = HttpContext.Session.GetString("Email") ?? "";
            string message = await _context.CaptureResult(result);

            if (message.Contains("successfully", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Results");
            }
            ViewBag.Message = message;
            return View(result);

        }//end of method
    }
}
