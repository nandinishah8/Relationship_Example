﻿using System.ComponentModel.DataAnnotations;

namespace Relationship_Example.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
