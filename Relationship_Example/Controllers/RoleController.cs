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
    public class RoleController : ControllerBase
    {
        private readonly RelationshipContext _dbContext;

        public RoleController(RelationshipContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Role
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _dbContext.Roles.Include(r => r.Users).ToList();
            return Ok(roles);
        }

        [HttpPost]
        public IActionResult CreateRole(Role role)
        {
            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();
            return Ok(role);
        }






        private bool RoleExists(int id)
        {
            return (_dbContext.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
