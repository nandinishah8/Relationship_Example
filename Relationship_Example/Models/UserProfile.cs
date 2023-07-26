using System.Collections.Generic;

namespace Relationship_Example.Models
{
    public class UserProfile
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
