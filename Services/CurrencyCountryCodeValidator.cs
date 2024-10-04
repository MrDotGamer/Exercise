using Exchange.Services.Interfaces;
using FluentValidation;

namespace Exchange.Services
{
    public class CurrencyCountryCodeValidator : AbstractValidator<string[]>
    {
        private readonly IXmlService _xmlService;
        public CurrencyCountryCodeValidator(IXmlService xmlService)
        {
            _xmlService = xmlService;
            RuleFor(args => new string[] { args[0], args[1] }).Must(IsCountryCodeExist)
                .WithMessage("Please check if the input alphabetic country codes exist or are available");
        }

        private bool IsCountryCodeExist(string[] codes)
        {
            return _xmlService.CheckCountryCode(codes);
        }
    }
}
