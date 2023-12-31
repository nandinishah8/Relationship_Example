﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relationship_Example.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
