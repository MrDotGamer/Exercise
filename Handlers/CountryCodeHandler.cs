using Exchange.Exceptions;
using Exchange.Services;
using Exchange.Services.Interfaces;

namespace Exchange.Handlers
{
    public class CountryCodeHandler(ICheckCountryCodeService xmlService) : ArgumentHandler
    {
        private readonly CurrencyCountryCodeValidator _currencyCountryCodeValidator = new(xmlService);
        public override async Task<string[]> HandleAsync(string[] args)
        {

            var result = await _currencyCountryCodeValidator.ValidateAsync(args);
            if (!result.IsValid)
            {
                throw new ValidateCurrencyCountryCodeArgumentsException(result.Errors);
            }
            return await base.HandleAsync(args);
        }
    }
}
