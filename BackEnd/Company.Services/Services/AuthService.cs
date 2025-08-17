using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Services.DTOS;
using Company.Services.DTOS.Common;
using Company.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Company.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<CompanyModel> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IOtpService _otpService;
        private readonly IOtpVerificationRepository _otpRepository;
        private readonly IWebHostEnvironment _env;
        private readonly ICompanyRepository _companyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(
            UserManager<CompanyModel> userManager,
            IJwtService jwtService,
            IOtpVerificationRepository otpRepository,
            IWebHostEnvironment env,
            IOtpService otpService ,ICompanyRepository companyRepository , IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _otpRepository = otpRepository;
            _env = env;
            _otpService = otpService;
            _companyRepository = companyRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        
        public async Task<Response<string>> RegisterCompanyAsync(RegisterCompanyRequestDto dto)
        {
            var existingCompany = await _companyRepository.GetAsync(c => c.Email == dto.Email);
            if (existingCompany != null)
                return Response<string>.Fail("Email already used");

            var logoPath = string.Empty;
            if (dto.Logo != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Logo.FileName)}";
                var rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var uploads = Path.Combine(rootPath, "logos");
                Directory.CreateDirectory(uploads);
                var filePath = Path.Combine(uploads, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.Logo.CopyToAsync(stream);
                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                logoPath = $"{baseUrl}/logos/{fileName}";
                

            }

            var company = new CompanyModel
            {
                UserName = dto.Email.Split('@')[0],
                Email = dto.Email,
                ArabicName = dto.ArabicName,
                EnglishName = dto.EnglishName,
                PhoneNumber = dto.PhoneNumber?.Trim(),
                WebsiteUrl = dto.WebsiteUrl?.Trim(),
                LogoUrl = string.IsNullOrEmpty(logoPath) ? null : logoPath,
                CreatedAt = DateTime.UtcNow
            };
            string password = Guid.NewGuid().ToString() + "Aa1#";
            var result = await _userManager.CreateAsync(company, password) ;
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Response<string>.Fail($"Failed to create company: {errors}");
            }
            var email = new SendOtpRequestDto
            {
                Email = company.Email,
            };
            var otp = await _otpService.SendOtpAsync(email);

            return Response<string>.Success(otp.Data , "Registration successful. OTP generated.");
        }

        public async Task<Response<string>> SetPasswordAsync(SetPasswordRequestDto dto)
        {
            var company = await _companyRepository.GetAsync(c => c.Email == dto.Email);
            if (company == null)
                return Response<string>.Fail("Company not found");

            if (!company.IsVerified)
                return Response<string>.Fail("Company not verified");

            if (dto.NewPassword != dto.ConfirmPassword)
                return Response<string>.Fail("Passwords do not match");

            var token = await _userManager.GeneratePasswordResetTokenAsync(company);
            var result = await _userManager.ResetPasswordAsync(company, token, dto.NewPassword);
            if (!result.Succeeded)
            {
                var errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return Response<string>.Fail(errorMessage);
            }

            return Response<string>.Success("Password set successfully");
        }

        public async Task<Response<LoginResponseDto?>> LoginAsync(LoginRequestDto dto)
        {
            var company = await _companyRepository.GetAsync(c => c.Email == dto.Email);
            if (company == null)
            {
                return Response<LoginResponseDto?>.Fail("Invalid email or password");
                
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(company, dto.Password);
            if (!isPasswordValid)
            {
                return Response<LoginResponseDto?>.Fail("Invalid email or password");
            }

            var token =await _jwtService.GenerateToken(company.Id, dto.Email);
            var loginResponse = new LoginResponseDto
            {
                Token = token,
                CompanyName = company.EnglishName,
                LogoUrl = company.LogoUrl
            };

            return new Response<LoginResponseDto?>
            {
                Data = loginResponse,
                Message = "Login successfully",
                Status = true
            };
        }

    }

}
