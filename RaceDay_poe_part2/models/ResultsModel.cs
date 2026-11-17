using System.ComponentModel.DataAnnotations;

namespace RaceDay_poe_part2.models
{//start of namespace
    public class ResultsModel
    {//start of class

        [Required]
        public int ResultId { get; set; }
        public string finishTime { get; set; } = string.Empty;
        public int position { get; set; }
        public string ResultStatus { get; set; } = string.Empty;
        public string RecordedAt { get; set; } = string.Empty;
        public int Enrollment_id { get; set; }
    }//end of class
}//end of namespace
