using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        // GET: api/UserProfile
        [HttpGet]
        public IActionResult GetUserProfiles()
        {
            var profiles = _dbContext.UserProfiles.ToList();
            return Ok(profiles);
        }

        [HttpPost]
        public IActionResult CreateUserProfile(UserProfile profile)
        {
            _dbContext.UserProfiles.Add(profile);
            _dbContext.SaveChanges();
            return Ok(profile);
        }

        



        private bool UserProfileExists(int id)
        {
            return (_dbContext.UserProfiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
