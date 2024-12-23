using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HyggyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }
        [HttpGet("exceptByRoleId")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRolesExceptByRoleId([FromQuery] string roleIds)
        {
            string[] ids = roleIds.Split('|');
            var roles = await _roleManager.Roles.Where(r => !ids.Contains(r.Id)).ToListAsync();
            return roles;
        }
        [HttpGet("exceptByRoleName")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRolesExceptByRoleName([FromQuery] string roleNames)
        {
            string[] names = roleNames.Split('|');
            var roles = await _roleManager.Roles.Where(r => !names.Contains(r.Name)).ToListAsync();
            return roles;
        }
        [HttpGet("byRoleId")]
        public async Task<ActionResult<IdentityRole>> GetRoleById([FromQuery] string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            return role;
        }
        [HttpGet("byRoleName")]
        public async Task<ActionResult<IdentityRole>> GetRoleByName([FromQuery] string roleName)
        {
            var role = await _roleManager.Roles.Where(r=>r.Name.Contains(roleName)).FirstOrDefaultAsync();
            if (role == null)
                return NotFound();
            return role;
        }
        [HttpGet("byRoleNames")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoleByNames([FromQuery] string roleNames)
        {
            string[] names = roleNames.Split('|');
            var roles = await _roleManager.Roles.Where(r => names.Contains(r.Name)).ToListAsync();
            return roles;
        }

    }
}
