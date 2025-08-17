using Company.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Entities
{
    public class OtpVerification : BaseEntity
    {
        
        public Guid CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public string OtpCode { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public bool IsUsed { get; set; }


    }

}
