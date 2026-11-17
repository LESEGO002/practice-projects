using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI_Act_1.Models;

namespace WebAppAPI_Act_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        Events events = new Events() 
        {//opening curly brace
            EventId =001,
            EventDescription= "Birthday Party",
            EventDate=31/07/2026

        };//closeing curly brace

        //creating http get method
        [HttpGet]

        //method to get the event details
        public IActionResult getEventInto() 
        {
            return Ok(events);
        }//end oth  mwthod

        //creating http post method
        [HttpPost]
        public string PosteventInfo(int EventId) {
            return "Event is created";
        }
    }
}
