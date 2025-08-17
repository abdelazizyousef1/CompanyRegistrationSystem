
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Services.DTOS;
using Company.Services.DTOS.Common;
using Company.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Services
{
    public class OtpService : IOtpService
    {
        private readonly UserManager<CompanyModel> _userManager;
        private readonly IOtpVerificationRepository _otpRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmailSender _emailSender;
        public OtpService(IOtpVerificationRepository otpRepository, IEmailSender emailSender , ICompanyRepository companyRepository , UserManager<CompanyModel> userManager)
        {
            _otpRepository = otpRepository;
            _emailSender = emailSender;
            _companyRepository = companyRepository;
            _userManager = userManager;
        }
        
        private async Task<OtpVerification> GenerateEmailOTP(Guid companyId)
        {
            string otpCode = GenerateSecureOtpCode();
            var now = DateTime.UtcNow;
            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(3);

            var existingOtp = await _otpRepository.GetAsync(o => o.CompanyId == companyId && !o.IsUsed);

            if (existingOtp != null)
            {
                existingOtp.OtpCode = otpCode;
                existingOtp.CreatedAt = now;
                existingOtp.ExpiresAt = expiresAt;
                 await _otpRepository.UpdateAsync(existingOtp);

                return existingOtp;
            }

            var newOtp = new OtpVerification
            {
                CompanyId = companyId,
                OtpCode = otpCode,
                CreatedAt = now,
                ExpiresAt = expiresAt,
                IsUsed = false
            };

            await _otpRepository.AddAsync(newOtp);
            return newOtp;
        }
        private string GenerateSecureOtpCode()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            var number = BitConverter.ToUInt32(bytes, 0) % 900000 + 100000;
            return number.ToString();
        }
        

        public async Task<Response<string>> SendOtpAsync(SendOtpRequestDto sendOtpRequestDto)
        {
            var company = await _companyRepository.GetAsync(c => c.Email == sendOtpRequestDto.Email);
            if (company == null)
                return Response<string>.Fail("Company not found");

            var otp = await GenerateEmailOTP(company.Id);

            string message = $"Verification code is {otp.OtpCode}.";
            await _emailSender.SendEmailAsync(company.Email, "OTP Verification", message);

            return Response<string>.Success(otp.OtpCode,"OTP sent successfully");
        }
        public async Task<Response<string>> VerifyOtpAsync(OtpVerificationRequestDto dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLowerInvariant();

            var company = await _companyRepository.GetAsync(c => c.Email.ToLower() == normalizedEmail);
            if (company == null)
                return Response<string>.Fail("No company is registered with this email.");

            var otp = await _otpRepository.GetAsync(o =>
                o.CompanyId == company.Id &&
                o.OtpCode == dto.OtpCode &&
                !o.IsUsed &&
                o.ExpiresAt > DateTimeOffset.UtcNow);

            if (otp == null)
                return Response<string>.Fail("The OTP is either invalid, expired, or already used.");

            otp.IsUsed = true;
            company.IsVerified = true;

            // Optionally wrap in a transaction if needed
            await _otpRepository.UpdateAsync(otp);
            await _companyRepository.UpdateAsync(company);

            return Response<string>.Success("OTP verified successfully.");
        }




    }
}
       


