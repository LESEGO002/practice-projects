using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using RaceDay_poe_part2.backend;
using RaceDay_poe_part2.models;
using System.ComponentModel;

namespace RaceDay_poe_part2.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {//start of class


        public enum events
        {

            Open_Circuit_TrackDay,
            Cars_vs_BikesTrackDay,
            National_CircuitRace_Meeting,
            Gauteng_DriftDay,
            Motorcycle_TrackDay,
            Open_PracticeSession,
            Classic_MotorsportFestival,
            Ladies_onTrack,

        }

        public enum location
        {
            Johannesburg,
            Pretoria,
            CapeTown,
            Durban,
            Bloemfontein,
            Polokwane,
            Mbombela,
            Rustenburg,
            Kimberley,
            Carletonville

        }

        EventsModel eve;

        //httpPost request to Get the organiser detailes from db
        [HttpPost("Organizers")]

        public string EventInfo(events EventName, string Eventdescription, DateTime EventDate, location EventLocation, int MaxParticipants, string Eventstatus,string organizerEmail,string categoryName)
        {//start of method

            
            //HttpContext.Session.SetString("description", Eventdescription);
            //HttpContext.Session.SetInt32("particpants", MaxParticipants);
           

            Events Eve = new Events();
            string Part = Eve.CreateEvents( EventName.ToString(),Eventdescription, EventDate,  EventLocation.ToString(), MaxParticipants, Eventstatus, organizerEmail, categoryName);

            return Part;

        }//end of method


        //HttpGet request to get event by Id
        [HttpGet("{EventId}")]

        public string event_byId(int EventId) 
        {
            Events EvId = new Events();
            string resId = EvId.EventbyID(EventId);

            return resId;
        }

        

        
        //HttpPut request to update event by Id
        [HttpPut("{EventId}")]

        public string Updateevent_byId(int EventId, string Email, events EventName, string EventDescription, DateTime EventDate, location EventLocation, int MaxParticipants, string Eventstatus)
        {
            Events EvUpId = new Events();
            string resUpId = EvUpId.UpdateEvent(EventId, Email,EventName.ToString(), EventDescription, EventDate, EventLocation.ToString(),MaxParticipants, Eventstatus);

            return resUpId;
        }


        [HttpDelete("{EventId}")]

        public string DeleteEvent_byId(int EventId, string Email)
        {
            Events EvDelId = new Events();
            string resDelId = EvDelId.DeleteEvent(EventId,Email);

            return resDelId;
        }

        //HttpGet request to get all events
        [HttpGet("all")]
        public List<EventsModel> GetAllEvents()
        {//start of method

            Events Eve = new Events();
            return Eve.GetAllEventsJson();

        }//end of method


        //HttpGet request to get event by Id as JSON
        [HttpGet("details/{EventId}")]
        public EventsModel GetEventByIdJson(int EventId)
        {//start of method

            Events Eve = new Events();
            return Eve.GetEventByIdJson(EventId);

        }//end of method


       
    }//end of class
}//edn of namespace
