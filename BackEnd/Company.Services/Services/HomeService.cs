using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Services.DTOS;
using Company.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Services
{
    public class HomeService : IHomeService
    {
        private readonly UserManager<CompanyModel> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICompanyRepository _companyRepository;

        public HomeService(UserManager<CompanyModel> userManager, IHttpContextAccessor httpContextAccessor ,ICompanyRepository companyRepository )
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _companyRepository = companyRepository;
        }

        public async Task<HomeResponseDto> GetHomeDataAsync()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                throw new UnauthorizedAccessException("User not authenticated.");

            var company = await _companyRepository.GetAsync(c => c.Email == email);

            if (company == null)
                throw new Exception("Company not found.");

            return new HomeResponseDto
            {
                LogoUrl = company.LogoUrl,
                Message = $"Hello {company.EnglishName}"
            };
        }

    }
}
