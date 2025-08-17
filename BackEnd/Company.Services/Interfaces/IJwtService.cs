using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateToken(Guid companyId, string email);
    }
}
