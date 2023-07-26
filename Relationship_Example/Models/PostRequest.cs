using System.ComponentModel.DataAnnotations;

namespace Relationship_Example.Models
{
    public class PostRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
