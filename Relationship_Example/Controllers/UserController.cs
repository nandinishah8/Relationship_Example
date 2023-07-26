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
    public class UserController : ControllerBase
    {
        private readonly RelationshipContext _dbContext;

        public UserController(RelationshipContext context)
        {
            _dbContext = context;
        }

        // GET api/user
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _dbContext.Users.Include(u => u.UserProfile).Include(u => u.Posts).Include(u => u.Roles).ToListAsync();
            return Ok(users);
        }


        // GET api/user/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _dbContext.Users.Include(u => u.UserProfile).Include(u => u.Posts).Include(u => u.Roles)
                                      .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _dbContext.Users.Include(u => u.UserProfile).Include(u => u.Posts).Include(u => u.Roles)
                                      .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Update properties from the request
            user.UserName = userRequest.UserName;
            user.Email = userRequest.Email;

            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            // Map the updated User entity to the UserResponse
            var userResponse = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
                
            };

            return Ok(userResponse);
        }


        // POST api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map the UserRequest to the User entity
            var user = new User
            {
                UserName = userRequest.UserName,
                Email = userRequest.Email
                // Map other properties from the request as needed
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Map the User entity to the UserResponse
            var userResponse = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
                // Map other properties to the response as needed
            };

            return Ok(userResponse);
        }


        // DELETE api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_dbContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
