
using System.ComponentModel.DataAnnotations;

namespace Relationship_Example.Models
{
    public class UserRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
