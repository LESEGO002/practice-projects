using System.ComponentModel.DataAnnotations;

namespace RaceDay_poe_part2.models
{
    public class OrganizersModel
    {
        [Required]
        public int OrganizerId { get; set; }
        public int User_id { get; set; }
    }
}
