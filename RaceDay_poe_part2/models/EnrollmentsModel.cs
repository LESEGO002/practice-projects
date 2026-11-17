using System.ComponentModel.DataAnnotations;

namespace RaceDay_poe_part2.models
{//start of namespace
    public class EnrollmentsModel
    {//start of class

        [Required]
        public int EnrollmentId { get; set; }
        public int BibNumber { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Enrollmentstatus { get; set; } = string.Empty;
        public int event_id { get; set; }
        public int Participant_id { get; set; }
        public string EventName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string ParticipantEmail { get; set; } = string.Empty;

    }//end of class
}//end of method
