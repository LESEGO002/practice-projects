using System.ComponentModel.DataAnnotations;

namespace Raceday_poe.Models
{//start of namespace event model
    public class EventModel
    {//start of class

        [Required]
        public int EventId { get; set; }


        [Required]
        public string EventName { get; set; } = string.Empty;


        [Required]
        public string Eventdescription { get; set; } = string.Empty;


        [Required]
        public DateTime EventDate { get; set; }


        [Required]
        public string EventLocation { get; set; } = string.Empty;


        [Required]
        public int MaxParticipants { get; set; }


        [Required]
        public string Eventstatus { get; set; } = string.Empty;



        public string createdAt { get; set; } = string.Empty;


      
        public int Organiser_id { get; set; }


        public int Category_id { get; set; }

        public string organizerEmail { get; set; } = string.Empty;
        public string categoryName { get; set; } = string.Empty;
       

    }//end of class
}//end of namespace
