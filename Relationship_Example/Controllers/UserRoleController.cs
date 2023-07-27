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
    public class UserRoleController : ControllerBase
    {
        private readonly RelationshipContext _dbContext;

        public UserRoleController(RelationshipContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/userrole
        [HttpGet]
        public async Task<IActionResult> GetUserRoles()
        {
            var userRoles = await _dbContext.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .ToListAsync();

            var userRoleResponses = userRoles.Select(ur => new UserRoleResponse
            {
                Id = ur.Id,
                UserId = ur.UserId,
                User = new UserResponse
                {
                    Id = ur.User.Id,
                    UserName = ur.User.UserName,
                    Email = ur.User.Email
                },
                RoleId = ur.RoleId,
                Role = new RoleResponse
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name
                }
            }).ToList();

            return Ok(userRoleResponses);
        }


        // GET api/userrole/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRole(int id)
        {
            var userRole = await _dbContext.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.Id == id);

            if (userRole == null)
            {
                return NotFound();
            }

            var userRoleResponse = new UserRoleResponse
            {
                Id = userRole.Id,
                UserId = userRole.UserId,
                User = new UserResponse
                {
                    Id = userRole.User.Id,
                    UserName = userRole.User.UserName,
                    Email = userRole.User.Email
                },
                
                RoleId = userRole.RoleId,
                Role = new RoleResponse
                {
                    Id = userRole.Role.Id,
                    Name = userRole.Role.Name
                }
            };

            return Ok(userRoleResponse);
        }

        // PUT api/userrole/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserRole(int id, UserRoleRequest userRoleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(ur => ur.Id == id);

            if (userRole == null)
            {
                return NotFound();
            }

            userRole.UserId = userRoleRequest.UserId;
            userRole.RoleId = userRoleRequest.RoleId;

            _dbContext.Entry(userRole).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            var userRoleResponse = new UserRoleResponse
            {
                Id = userRole.Id,
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                User = new UserResponse
                {
                    Id = userRole.User.Id,
                    UserName = userRole.User.UserName,
                    Email = userRole.User.Email
                },
                Role = new RoleResponse
                {
                    Id = userRole.Role.Id,
                    Name = userRole.Role.Name
                }
            };

            return Ok(userRoleResponse);
        }




        // POST api/userrole
        [HttpPost]
        public async Task<IActionResult> CreateUserRole(UserRoleRequest userRoleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the User and Role exist in the database
            var user = await _dbContext.Users.FindAsync(userRoleRequest.UserId);
            var role = await _dbContext.Roles.FindAsync(userRoleRequest.RoleId);

            if (user == null || role == null)
            {
                return BadRequest("Invalid UserId or RoleId specified.");
            }


            var userRole = new UserRole
            {
                UserId = userRoleRequest.UserId,
                RoleId = userRoleRequest.RoleId
            };

            _dbContext.UserRoles.Add(userRole);
            await _dbContext.SaveChangesAsync();

            var userRoleResponse = new UserRoleResponse
            {
                Id = userRole.Id,
                UserId = userRole.UserId,
                User = new UserResponse
                {
                    Id = userRole.User.Id,
                    UserName = userRole.User.UserName,
                    Email = userRole.User.Email
                },
                RoleId = userRole.RoleId,

                Role = new RoleResponse
                {
                    Id = userRole.Role.Id,
                    Name = userRole.Role.Name
                }
            };

            return Ok(userRoleResponse);
        }

        // DELETE api/userrole/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(ur => ur.Id == id);

            if (userRole == null)
            {
                return NotFound();
            }

            _dbContext.UserRoles.Remove(userRole);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRoleExists(int id)
        {
            return (_dbContext.UserRoles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
