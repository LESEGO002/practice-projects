using System.ComponentModel.DataAnnotations;

namespace RaceDay_poe_part2.models
{//start of namespace
    public class CatergoriesModel
    {//start of class

        [Required]
        public int CatergoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescrition { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int organizerID { get; set; }


    }//end of class
}//end of namespace
