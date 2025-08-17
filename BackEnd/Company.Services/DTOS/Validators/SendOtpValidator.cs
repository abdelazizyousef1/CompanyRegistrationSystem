using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Company.Services.DTOS.Validators
{
    public class SendOtpValidator : AbstractValidator<SendOtpRequestDto>
    {
        public SendOtpValidator() 
        {

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        }
    }
}
