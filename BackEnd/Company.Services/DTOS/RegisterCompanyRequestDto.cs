using Microsoft.AspNetCore.Http;

namespace Company.Services.DTOS
{
    public class RegisterCompanyRequestDto
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }
        public IFormFile? Logo { get; set; }
    }

}
