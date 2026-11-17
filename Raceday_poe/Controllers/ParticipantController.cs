using Microsoft.AspNetCore.Mvc;
using Raceday_poe.Models;
using Raceday_poe.Servicers;

namespace Raceday_poe.Controllers
{
    public class ParticipantController : Controller
    {


        //start of field
        private readonly Api_sercices _context;
        //end of field

        //start of constructor
        public ParticipantController(Api_sercices context)
        {
            _context = context;
        }
        //end of constructor

        //start of method
        public async Task<IActionResult> ParticipantHome(string? search, string? status)
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            List<EventModel> events = await _context.GetAllEvents();

            if (!string.IsNullOrEmpty(search))
            {
                events = events.Where(e => e.EventName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                           e.EventLocation.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                events = events.Where(e => e.Eventstatus.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.Search = search;
            ViewBag.Status = status;
            return View(events);

        }//end of method

        //start of method
        public async Task<IActionResult> EventDetail(int id)
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            EventModel? ev = await _context.GetEventById(id);
            if (ev == null)
            {
                return NotFound();
            }

            List<CategoryModel> categories = await _context.GetAllCategories();
            ViewBag.Categories = categories;
            return View(ev);

        }//end of method

        //start of method
        [HttpPost]
        public async Task<IActionResult> Enrol(int eventId, int categoryId, int BibNumber, string eventName)
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            EnrollmentModel enroll = new EnrollmentModel();
            enroll.participantEmail = HttpContext.Session.GetString("Email") ?? "";
            enroll.eventId = eventId;
            enroll.categoryId = categoryId;
            enroll.BibNumber = BibNumber;
            enroll.eventName = eventName;
            enroll.Enrollmentstatus = "Registered";
            string message = await _context.CreateEnrollment(enroll);

            if (message.Contains("successfully", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("MyEnrollments");
            }
            TempData["Message"] = message;
            return RedirectToAction("EventDetail", new { id = eventId });

        }//end of method

        //start of method
        public async Task<IActionResult> MyEnrollments()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                return RedirectToAction("Login", "Home");

            string email = HttpContext.Session.GetString("Email") ?? "";
            List<EnrollmentModel> enrollments = await _context.GetMyEnrollments(email);
            return View(enrollments);
        }

        //start of method
        public async Task<IActionResult> MyResults()
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            string email = HttpContext.Session.GetString("Email") ?? "";
            List<ResultModel> results = await _context.GetMyResults(email);
            return View(results);

        }//end of method

        //start of method
        public async Task<IActionResult> Profile()
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            string email = HttpContext.Session.GetString("Email") ?? "";
            UserProfileModel profile = await _context.GetUserProfile(email);
            return View(profile);

        }//end of method

        //start of method
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileModel profile)
        {//start of method

            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            profile.Email = HttpContext.Session.GetString("Email") ?? "";
            string message = await _context.UpdateUserProfile(profile);

            if (message.Contains("successfully", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Message = "Profile updated successfully!";
            }
            else
            {
                ViewBag.Message = message;
            }
            return View(profile);

        }//end of method
    }
}
