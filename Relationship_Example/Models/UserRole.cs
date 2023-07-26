using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relationship_Example.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Role")]
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
