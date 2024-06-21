using BusinessLogicLayer;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/roles")]
    [ApiController]
    public class RoleController : ControllerBase    
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("admins")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _roleService.GetUsersWithRole(RoleConstants.Admin);
            return Ok(admins);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _roleService.GetUsersWithRole(RoleConstants.User);
            return Ok(users);
        }
    }
}
