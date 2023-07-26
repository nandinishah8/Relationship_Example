using System.ComponentModel.DataAnnotations;

namespace Relationship_Example.Models
{
    public class RoleRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
