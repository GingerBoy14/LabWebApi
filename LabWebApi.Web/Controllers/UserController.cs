using LabWebAPI.Contracts.Data;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Profile;
using LabWebAPI.Contracts.Services;
using LabWebAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LabWebApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private IUserService _userService;
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;
        public UserController(IUserService userService,IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
       
        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] UserUploadImageDTO file)
        {
            await _userService.UploadAvatar(file.Image, UserId);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        [Route("avatar")]
        public async Task<FileResult> GetImageAsync()
        {
            var file = await _userService.GetUserImageAsync(UserId);
            return File(file.Content, file.ContentType, file.Name);
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfileData()
        {
            var result = await _userService.GetUserProfileAsync(UserId);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> EditUserProfileData([FromBody] UserProfileDTO model)
        {
           
            var result = await _userService.EditUserProfileDataAsync(model);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("profile")]
        public async Task<IActionResult> DeleteUserProfile()
        {
            await _userService.DeleteUserProfileAsync(UserId);
            return Ok();
        }
        [Authorize]
        [HttpPost("profile/change-password")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UserPasswordDTO request)
        {
            await _userService.ChangeUserPassword(UserId, request.currentPassword, request.newPassword);
            return Ok();
        }
   
    }

}