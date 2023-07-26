using System.Collections.Generic;

namespace Relationship_Example.Models
{
    public class User
    {
      
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }

            public UserProfile UserProfile { get; set; }
            public ICollection<Post> Posts { get; set; }
            public ICollection<Role> Roles { get; set; }
    }
}

