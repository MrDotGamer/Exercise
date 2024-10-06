using Exchange.Exceptions;
using Exchange.Services;

namespace Exchange.Handlers
{
    public class AlphabeticalCountryCodeHandler() : ArgumentHandler
    {
        private readonly CurrencyAlphabeticCountryCodeValidator _alphabeticCountryCodeValidator = new();
        public override async Task<string[]> HandleAsync(string[] args)
        {
            var result = _alphabeticCountryCodeValidator.Validate(args);
            if (!result.IsValid)
            {
                throw new AlphabeticCountryCodeValidationException(result.Errors);
            }
            return await base.HandleAsync([args[0][..3].ToUpper(), args[0][4..7].ToUpper(), args[1]]);
        }
    }
}
