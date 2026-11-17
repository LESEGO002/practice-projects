using System.ComponentModel.DataAnnotations;

namespace Raceday_poe.Models
{
    public class UserProfileModel
    {

        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int age { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        public string UserRole { get; set; } = string.Empty;

    }
}
