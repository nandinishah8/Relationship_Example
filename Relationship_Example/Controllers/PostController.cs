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

        // GET api/post
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _dbContext.Posts.Include(p => p.User).ToListAsync();
            return Ok(posts);
        }


        // GET api/post/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _dbContext.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var postResponse = new PostResponse
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
                User = new UserResponse
                {
                    Id = post.User.Id,
                    UserName = post.User.UserName,
                    Email = post.User.Email
                }
            };

            return Ok(postResponse);
        }

        // PUT api/post/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostRequest postRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _dbContext.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            post.Title = postRequest.Title;
            post.Content = postRequest.Content;
            post.UserId = postRequest.UserId;


            _dbContext.Entry(post).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            var postResponse = new PostResponse
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
                User = new UserResponse
                {
                    Id = post.User.Id,
                    UserName = post.User.UserName,
                    Email = post.User.Email
                }
            };

            return Ok(postResponse);
        }

        // POST api/post
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostRequest postRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the specified UserId exists in the Users table
            var user = await _dbContext.Users.FindAsync(postRequest.UserId);
            if (user == null)
            {
                return BadRequest("Invalid UserId specified in the post.");
            }

            var post = new Post
            {
                Title = postRequest.Title,
                Content = postRequest.Content,
                UserId = postRequest.UserId
            };

            // Make sure to include the User object in the post when adding it to the context
            _dbContext.Posts.Add(post);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            var postResponse = new PostResponse
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
                User = new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                }
            };

            return Ok(postResponse);
        }



        // DELETE api/post/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return (_dbContext.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
