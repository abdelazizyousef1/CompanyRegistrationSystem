using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.DTOS
{
    public class OtpVerificationResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public static OtpVerificationResult Success() => new OtpVerificationResult
        {
            IsSuccess = true
        };

        public static OtpVerificationResult Fail(string errorMessage) => new OtpVerificationResult
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }

}
