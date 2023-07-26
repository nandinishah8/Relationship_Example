using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relationship_Example.Models
{
    public class UserProfile
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
