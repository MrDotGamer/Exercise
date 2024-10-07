using Exchange.Application.Exceptions;
using Exchange.Application.Services;
using Exchange.Application.Validators;
using Exchange.Services.Interfaces;

namespace Exchange.Application.Handlers
{
    public class CountryCodeValidationHandler(ICheckCountryCodeService xmlService) : ArgumentHandlerService
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
