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
        public IActionResult GetUserProfile(int id)
        {
            var userProfile = _dbContext.UserProfiles.Include(up => up.User).FirstOrDefault(up => up.Id == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            var userProfileResponse = new UserProfileResponse
            {
                Id = userProfile.Id,
                FullName = userProfile.FullName,
                BirthDate = userProfile.BirthDate
                // Map other properties to the response as needed
            };

            return Ok(userProfileResponse);
        }


        // PUT api/userprofile/5
        [HttpPut("{id}")]
        public IActionResult UpdateUserProfile(int id, UserProfileRequest userProfileRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProfile = _dbContext.UserProfiles.Include(up => up.User).FirstOrDefault(up => up.Id == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            userProfile.FullName = userProfileRequest.FullName;
            userProfile.BirthDate = userProfileRequest.BirthDate;
            // Update other properties as needed

            _dbContext.SaveChanges();

            var userProfileResponse = new UserProfileResponse
            {
                Id = userProfile.Id,
                FullName = userProfile.FullName,
                BirthDate = userProfile.BirthDate
                // Map other properties to the response as needed
            };

            return Ok(userProfileResponse);
        }


        // POST api/userprofile
        [HttpPost]
        public IActionResult CreateUserProfile(UserProfileRequest userProfileRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProfile = new UserProfile
            {
                FullName = userProfileRequest.FullName,
                BirthDate = userProfileRequest.BirthDate
                // Map other properties from the request as needed
            };

            _dbContext.UserProfiles.Add(userProfile);
            _dbContext.SaveChanges();

            var userProfileResponse = new UserProfileResponse
            {
                Id = userProfile.Id,
                FullName = userProfile.FullName,
                BirthDate = userProfile.BirthDate
                // Map other properties to the response as needed
            };

            return Ok(userProfileResponse);
        }

        /// DELETE api/userprofile/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUserProfile(int id)
        {
            var userProfile = _dbContext.UserProfiles.FirstOrDefault(up => up.Id == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            _dbContext.UserProfiles.Remove(userProfile);
            _dbContext.SaveChanges();

            return NoContent();
        }

        private bool UserProfileExists(int id)
        {
            return (_dbContext.UserProfiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
