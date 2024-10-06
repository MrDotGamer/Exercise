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
            RuleFor(args => new string[] { args[0], args[1] }).MustAsync(async (args, cancellationToken) => await IsCountryCodeExist(args))
                .WithMessage("Please check if the input alphabetic country codes exist or are available");
        }

        private async Task<bool> IsCountryCodeExist(string[] codes)
        {
            return await _xmlService.CheckCountryCodeAsync(codes);
        }
    }
}
