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
    public class UserProfileController : ControllerBase
    {
        private readonly RelationshipContext _dbContext;

        public UserProfileController(RelationshipContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/userprofile
        [HttpGet]
        public IActionResult GetUserProfiles()
        {
            var userProfiles = _dbContext.UserProfiles.Include(up => up.User);
            return Ok(userProfiles);
        }


        // GET api/userprofile/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var userProfile = await _dbContext.UserProfiles.Include(up => up.User)
                                     .FirstOrDefaultAsync(up => up.Id == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            var userProfileResponse = new UserProfileResponse
            {
                Id = userProfile.Id,
                FullName = userProfile.FullName,
                BirthDate = userProfile.BirthDate
                
            };

            return Ok(userProfileResponse);
        }


        // PUT api/userprofile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, UserProfileRequest userProfileRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProfile = await _dbContext.UserProfiles.Include(up => up.User)
                                       .FirstOrDefaultAsync(up => up.Id == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            userProfile.FullName = userProfileRequest.FullName;
            userProfile.BirthDate = userProfileRequest.BirthDate;
           

            _dbContext.Entry(userProfile).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            var userProfileResponse = new UserProfileResponse
            {
                Id = userProfile.Id,
                FullName = userProfile.FullName,
                BirthDate = userProfile.BirthDate
               
            };

            return Ok(userProfileResponse);
        }

        // POST api/userprofile
        [HttpPost]
        public async Task<IActionResult> CreateUserProfile(UserProfileRequest userProfileRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == userProfileRequest.UserId);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            var userProfile = new UserProfile
            {
                FullName = userProfileRequest.FullName,
                BirthDate = userProfileRequest.BirthDate,
                UserId = userProfileRequest.UserId

            };

            _dbContext.UserProfiles.Add(userProfile);
            await _dbContext.SaveChangesAsync();

            var userProfileResponse = new UserProfileResponse
            {
                Id = userProfile.Id,
                FullName = userProfile.FullName,
                BirthDate = userProfile.BirthDate
              
            };

            return Ok(userProfileResponse);
        }


        /// DELETE api/userprofile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            var userProfile = await _dbContext.UserProfiles.FirstOrDefaultAsync(up => up.Id == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            _dbContext.UserProfiles.Remove(userProfile);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProfileExists(int id)
        {
            return (_dbContext.UserProfiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
