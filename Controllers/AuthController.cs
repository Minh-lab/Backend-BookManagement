using BackendAPIASP.DTOs.Authentication;
using BackendAPIASP.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPIASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AuthController(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _accountService.RegisterUserAsync(registerDto);

            if (result == null)
                return BadRequest("Email đã tồn tại hoặc đăng ký thất bại.");

            return Ok(result); // Trả về NewUserDto (kèm Token)
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _accountService.AuthenticateAsync(loginDto);
            if (result == null)
                return BadRequest("Đăng nhập thất bại.");
            return Ok(result);
        }
    }
}
