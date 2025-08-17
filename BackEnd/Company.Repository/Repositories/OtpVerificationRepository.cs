using Company.Data;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class OtpVerificationRepository : GenericRepository<OtpVerification>, IOtpVerificationRepository
    {
        
        public OtpVerificationRepository(ApplicationDbContext context) : base(context)
        {
        }
        

    }
}
