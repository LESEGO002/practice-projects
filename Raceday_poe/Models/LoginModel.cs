using System.ComponentModel.DataAnnotations;

namespace Raceday_poe.Models
{//start of namspace
    public class LoginModel
    {//start of class

        [Required(ErrorMessage = "Your email is required!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "password is required!")]
        public string passwordHashed { get; set; }
    }//end of class
}//end of namespace
