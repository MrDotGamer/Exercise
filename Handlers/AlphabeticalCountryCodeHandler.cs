using Exchange.Exceptions;
using Exchange.Services;

namespace Exchange.Handlers
{
    public class AlphabeticalCountryCodeHandler() : ArgumentHandler
    {
        private readonly CurrencyAlphabeticCountryCodeValidator _alphabeticCountryCodeValidator = new();
        public override string[] Handle(string[] args)
        {
            var result = _alphabeticCountryCodeValidator.Validate(args);
            if (!result.IsValid)
            {
                throw new AlphabeticCountryCodeValidationException(result.Errors);
            }
            return base.Handle([args[0][..3].ToUpper(), args[0][4..7].ToUpper(), args[1]]);
        }
    }
}
