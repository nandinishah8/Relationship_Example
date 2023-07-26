using System.ComponentModel.DataAnnotations;

namespace Relationship_Example.Models
{
    public class UserRoleRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
