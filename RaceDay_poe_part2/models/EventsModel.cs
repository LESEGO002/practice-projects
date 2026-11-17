using System.ComponentModel.DataAnnotations;

namespace RaceDay_poe_part2.models
{//start of namespace
    public class EventsModel
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


        [Required]
        public string createdAt { get; set; } = string.Empty;


        [Required]
        public int Organiser_id { get; set; }

        [Required]
        public int Category_id { get; set; }

    }//end of class
}//end of namespace
