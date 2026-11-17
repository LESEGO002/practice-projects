using System.ComponentModel.DataAnnotations;

namespace RaceDay_poe_part2.models
{//start of namespace
    public class ParticipantsModel
    {//start of class

        [Required]
        public int ParticipantId { get; set; }
        public int event_id { get; set; }
        public int User_id { get; set; }

    }//end of class
}//end of namespace
