using System.ComponentModel.DataAnnotations;

namespace proj_MVC.Models
{
    public class User
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Name Cannot be empty, Please fill in!")]
        public string name { get; set; }
        [Required]
        public string surname { get; set; }
        [Required(ErrorMessage ="Please fill in the age!")]
        [Range(10,50, ErrorMessage ="Age must be between 10 - 120")]
        public int age { get; set; }
    }
}
