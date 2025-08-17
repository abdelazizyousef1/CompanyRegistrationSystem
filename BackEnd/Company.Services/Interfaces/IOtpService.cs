using Company.Services.DTOS;
using Company.Services.DTOS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Interfaces
{
    public interface IOtpService
    {
        
         Task<Response<string>> SendOtpAsync(SendOtpRequestDto sendOtpRequestDto);
         Task<Response<string>> VerifyOtpAsync(OtpVerificationRequestDto dto);

    }

}
