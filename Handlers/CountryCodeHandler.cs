using Exchange.Exceptions;
using Exchange.Services;
using Exchange.Services.Interfaces;

namespace Exchange.Handlers
{
    public class CountryCodeHandler(IXmlService xmlService) : ArgumentHandler
    {
        private readonly CurrencyCountryCodeValidator _currencyCountryCodeValidator = new(xmlService);
        public override string[] Handle(string[] args)
        {

            var result = _currencyCountryCodeValidator.Validate(args);
            if (!result.IsValid)
            {
                throw new ValidateCurrencyCountryCodeArgumentsException(result.Errors);
            }
            return base.Handle(args);
        }
    }
}
