using Exchange.Exceptions;
using Exchange.Services;

namespace Exchange.Extensions
{
    public static class ExchangeExtensions
    {
        private static readonly CurrencyAmountArgsValidator Validator = new();
        private static readonly CurrencyAlphabeticCountryCodeValidator AlphabeticCountryCodeValidator = new();
        private static readonly ArgumentsValidator ArgumentsValidator = new();
        private static readonly CurrencyCountryCodeValidator CurrencyCountryCodeValidator = new(new XmlCountryCodeService());
        public static string[] TryParseAlphabeticalCountryCodeArgument(this string[] args)
        {
            var result = AlphabeticCountryCodeValidator.Validate(args);
            if (!result.IsValid)
            {
                throw new AlphabeticCountryCodeValidationException(result.Errors);
            }
            return [args[0][..3].ToUpper(), args[0][4..7].ToUpper(), args[1]];
        }

        public static void ValidateArguments(this string[] args)
        {
            var result = ArgumentsValidator.Validate(args);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
        public static void TryParseCurrencyArguments(this string[] args)
        {
            var result = Validator.Validate(args);
            if (!result.IsValid)
            {
                throw new ParseCurrencyArgumentsException(result.Errors);
            }

            args[2] = args[2].Replace(",", ".");
        }

        public static void ValidateCurrencyCountryCodeArguments(this string[] args)
        {
            var result = CurrencyCountryCodeValidator.Validate(args);
            if (!result.IsValid)
            {
                throw new ValidateCurrencyCountryCodeArgumentsException(result.Errors);
            }
        }
    }
}
