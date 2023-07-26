using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relationship_Example.Data;
using Relationship_Example.Models;

namespace Relationship_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly RelationshipContext _dbContext;

        public PostController(RelationshipContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var posts = _dbContext.Posts.ToList();
            return Ok(posts);
        }

        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            return Ok(post);
        }

        private bool PostExists(int id)
        {
            return (_dbContext.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
