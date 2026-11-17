using Microsoft.AspNetCore.Mvc;
using Raceday_poe.Models;
using Raceday_poe.Servicers;


namespace Raceday_poe.Controllers
{
    public class EventController : Controller
    {

        //start of field
        private readonly Api_sercices _context;
        //end of field

        //start of constructor
        public EventController(Api_sercices context)
        {
            _context = context;
        }
        //end of constructor

        //start of method
        public async Task<IActionResult> Events()
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            List<EventModel> events = await _context.GetAllEvents();
            return View(events);

        }//end of method

        //start of method
        public async Task<IActionResult> EventDetails(int id)
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
            return View(ev);

        }//end of method

        //start of method
        public IActionResult CreateEvent()
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            return View(new EventModel());

        }//end of method

        //start of method
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventModel ev)
        {//start of method

            if (!ModelState.IsValid)
            {
                return View(ev);
            }

            ev.organizerEmail = HttpContext.Session.GetString("Email") ?? "";
            string message = await _context.CreateEvent(ev);

            if (message.Contains("Thank your event has been created...", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Events");
            }
            ViewBag.Message = message;
            return View(ev);

        }//end of method

        //start of method
        public async Task<IActionResult> EditEvent(int id)
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            EventModel? ev = await _context.GetEventById(id);
            if (ev == null)
            {
                return Content("Event Not found for Id=" + id);
            }
            return View(ev);

        }//end of method

        //start of method
        [HttpPost]
        public async Task<IActionResult> EditEvent(int id, EventModel ev)
        {//start of method

            if (!ModelState.IsValid)
            {
                return View(ev);
            }

            ev.organizerEmail = HttpContext.Session.GetString("Email") ?? "";
            string message = await _context.UpdateEvent(id, ev);

            if (message.Contains("successfully", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Events");
            }
            ViewBag.Message = message;
            return View(ev);

        }//end of method

        //start of method
        public async Task<IActionResult> DeleteEvent(int id)
        {//start of method

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
            {
                return RedirectToAction("Login", "Home");
            }

            EventModel? ev = await _context.GetEventById(id);
            if (ev == null)
            {
                return Content("Event not found for Id=" + id);
            }
            return View(ev);

        }//end of method

        //start of method
        [HttpPost]
        public async Task<IActionResult> DeleteEventConfirmed(int id)
        {//start of method

            string message = await _context.DeleteEvent(id);
            return RedirectToAction("Events");

        }//end of method
    }
}
