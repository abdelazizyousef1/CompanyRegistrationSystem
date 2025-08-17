using Company.Services.DTOS;
using Company.Services.DTOS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Response<string>> RegisterCompanyAsync(RegisterCompanyRequestDto dto);
        Task<Response<string>> SetPasswordAsync(SetPasswordRequestDto dto);
        Task<Response<LoginResponseDto?>> LoginAsync(LoginRequestDto dto);
    }

}
