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

        // GET api/role
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _dbContext.Roles.ToListAsync();
            return Ok(roles);
        }


        // GET api/role/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            var roleResponse = new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
                // Add other properties as needed
            };

            return Ok(roleResponse);
        }



        // PUT api/role/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleRequest roleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            role.Name = roleRequest.Name;
            // Update other properties as needed

            _dbContext.Entry(role).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            var roleResponse = new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
                // Add other properties as needed
            };

            return Ok(roleResponse);
        }



        // POST api/role
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleRequest roleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new Role
            {
                Name = roleRequest.Name
                // Add other properties from the request as needed
            };

            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();

            var roleResponse = new RoleResponse
            {
                Id = role.Id,
                Name = role.Name
                // Add other properties as needed
            };

            return Ok(roleResponse);
        }

        // DELETE api/role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return (_dbContext.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
