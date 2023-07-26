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
    public class UsersController : ControllerBase
    {
        private readonly RelationshipContext _dbcontext;

        public UsersController(RelationshipContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _dbcontext.Users.Include(u => u.UserProfile).ToList();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();
            return Ok(user);
        }
    }
}
