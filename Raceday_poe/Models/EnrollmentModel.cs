using System.ComponentModel.DataAnnotations;

namespace Raceday_poe.Models
{
    public class EnrollmentModel
    {

        public int enrollmentId { get; set; }
        public string participantEmail { get; set; } = string.Empty;
        public int eventId { get; set; }
        public int categoryId { get; set; }
        public int event_id { get; set; }
        public int Participant_id { get; set; }
        public string Enrollmentstatus { get; set; } = string.Empty;
        public string eventName { get; set; } = string.Empty;
        public string categoryName { get; set; } = string.Empty;
        public DateTime eventDate { get; set; }
        public string participantName { get; set; } = string.Empty;
        public int BibNumber { get; set; }
       
        
    }
}
