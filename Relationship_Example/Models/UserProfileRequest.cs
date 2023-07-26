using System.ComponentModel.DataAnnotations;

namespace Relationship_Example.Models
{
    public class UserProfileRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
