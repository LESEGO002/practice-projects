using System.ComponentModel.DataAnnotations;

namespace Raceday_poe.Models
{
    public class CategoryModel
    {

        public int catergoryId { get; set; }

        [Required]
        public string categoryName { get; set; } = string.Empty;

        [Required]
        public string categoryDescrition { get; set; } = string.Empty;

        [Required]
        public int minAge { get; set; }

        [Required]
        public int maxAge { get; set; }

        public int organizerID { get; set; }
        public string organizerEmail { get; set; } = string.Empty;
    }
}
