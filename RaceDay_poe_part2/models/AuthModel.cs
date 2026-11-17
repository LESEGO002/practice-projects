using System.ComponentModel.DataAnnotations;

namespace RaceDay_poe_part2.models
{//start of namespace
    public class AuthModel
    {//start of class

        [Required]
        public int Userid { get; set; }

        [Required(ErrorMessage = "please enter your name!")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "please enter your surname!")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Enter you age!!")]
        [Range(10, 50, ErrorMessage = "Age must be between 10 - 120")]
        public int age { get; set; }

        [Required(ErrorMessage = "Your email is required!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "password is required!")]
        public string passwordHashed { get; set; }


        [Required]
        public string createdAt { get; set; }


        [Required(ErrorMessage = "Please choose a Role!")]
        public string UserRole { get; set; }

    }//end of class

}//end of namespace
