using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace Company.Services.DTOS.Validators
{
    

    public class CompanyRegisterValidator : AbstractValidator<RegisterCompanyRequestDto>
    {
        public CompanyRegisterValidator()
        {
            RuleFor(x => x.ArabicName)
                .NotEmpty().WithMessage("Arabic company name is required.");

            RuleFor(x => x.EnglishName)
                .NotEmpty().WithMessage("English company name is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^01[0125][0-9]{8}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.WebsiteUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var _))
                .When(x => !string.IsNullOrEmpty(x.WebsiteUrl))
                .WithMessage("Invalid website URL.");
        }
    }

}
