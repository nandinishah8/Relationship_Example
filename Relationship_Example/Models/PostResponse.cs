using System.ComponentModel.DataAnnotations;

namespace Relationship_Example.Models
{
    public class PostResponse
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        public UserResponse User { get; set; }
    }
}
