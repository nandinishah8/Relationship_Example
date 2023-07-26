using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Relationship_Example.Models
{
    public class User
    {

            [Key]
            public int Id { get; set; }

            [Required]
            public string UserName { get; set; }

            [Required]
            public string Email { get; set; }

            
            public UserProfile UserProfile { get; set; }
            public ICollection<Post> Posts { get; set; }
            public ICollection<Role> Roles { get; set; }
    }
}

