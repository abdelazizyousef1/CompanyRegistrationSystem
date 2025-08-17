using Company.Services.DTOS;
using Company.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _otpService;
        public OtpController(IOtpService otpService)
        {
            _otpService = otpService;
        }
        [HttpPost("sendOTP")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequestDto sendOtpRequestDto)
        {
            try
            {
                var result = await _otpService.SendOtpAsync(sendOtpRequestDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOtp(OtpVerificationRequestDto otpVerificationRequestDto)
        {
            try
            {
               var result=await _otpService.VerifyOtpAsync(otpVerificationRequestDto);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
