using System.ComponentModel.DataAnnotations;

namespace Raceday_poe.Models
{
    public class ResultModel
    {

        public int resultId { get; set; }
        public int enrollmentId { get; set; }
        public string finishTime { get; set; } = string.Empty;
        public int position { get; set; }
        public int totalParticipants { get; set; }
        public string eventName { get; set; } = string.Empty;
        public DateTime eventDate { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public string participantEmail { get; set; } = string.Empty;
        public int BibNumber { get; set; }
        public string ResultStatus { get; set; } = string.Empty;
        public string organizerEmail { get; set; } = string.Empty;
        public int enrollment_id { get; set; }
        public string recordedAt { get; set; } = string.Empty;
       
    }
}
