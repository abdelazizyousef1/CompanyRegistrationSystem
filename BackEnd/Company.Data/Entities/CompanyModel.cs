using Company.Data.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Entities
{
    public class CompanyModel : IdentityUser<Guid>
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LogoUrl { get; set; }
        public string? Password { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<OtpVerification> Otps { get; set; }
    }
}
