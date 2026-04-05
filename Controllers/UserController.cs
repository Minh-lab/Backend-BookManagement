using BackendAPIASP.DTOs.User_Profile;
using BackendAPIASP.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPIASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet("profile/{userId}")]
        public async Task<ActionResult<UserProfileDto?>> GetUserProfile(int userId)
        {
            UserProfileDto? uf = await _userService.GetUserProfileAsync(userId);
             return Ok(uf);
        }
    }
}
