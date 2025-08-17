using Company.Services.DTOS;
using Company.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _AuthService;

        public AuthController(IAuthService AuthService)
        {
            _AuthService = AuthService;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterCompanyRequestDto registerRequest)
        {
            try
            {
                var result = await _AuthService.RegisterCompanyAsync(registerRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                var result = await _AuthService.LoginAsync(loginRequest);
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest($"Login failed: {ex.Message}");
            }
        }

        
        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordRequestDto setPasswordRequest)
        {
            try
            {
                var result =await _AuthService.SetPasswordAsync(setPasswordRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Setting password failed: {ex.Message}");
            }
        }
    }
}
