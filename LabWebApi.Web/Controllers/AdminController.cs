using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabWebApi.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _adminService.GetUsersAsync();
            return Ok(result);
        }
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _adminService.GetUserByIdAsync(id);
            return Ok(result);
        }
        [HttpPut("users")]
        public async Task<IActionResult> EditUser([FromBody] UserInfoDTO model)
        {
            var result = await _adminService.EditUserAsync(model);
            return Ok(result);
        }
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _adminService.DeleteUserAsync(id);
            return Ok();
        }
    }
}
