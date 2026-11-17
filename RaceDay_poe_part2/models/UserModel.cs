using System.ComponentModel.DataAnnotations;

namespace RaceDay_poe_part2.models
{//start of namespace
    public class UserModel
    {//end of namespace
        [Required]
        public int Userid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int age { get; set; }
        public string Email { get; set; }
        

    }//end of class
}//end of namespace
