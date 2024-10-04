using Exchange.Exceptions;
using Exchange.Utilities;
using System.Text.RegularExpressions;

namespace Exchange.Extensions
{
    public static class ExchangeExtensions
    {
        public static string[] CheckArgumentsForExchangeService(this string[] args)
        {
            args.IsValidArgumentCount();
            var updatedArgs = args.GetCountriesAlfabeticCode();
            updatedArgs.AreExistingCountryCodes();
            updatedArgs.TryParseCurrency();

            return updatedArgs;
        }

        private static void IsValidArgumentCount(this string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentCountException("Exactly 2 arguments are required, command example: xxx/yyy amount");
            }
        }

        private static void TryParseCurrency(this string[] args)
        {
            const string pattern = @"^(\d+([,.]\d{0,2})?|[,.]\d{1,2})$";
            if (!Regex.IsMatch(args[2], pattern))
            {
                throw new BadMoneyFormatException("Money format is not valid");
            }
            args[2] = args[2].Replace(",", ".");
        }

        private static void AreExistingCountryCodes(this string[] args)
        {
            if (!IsCountryCodeExist(args[..2]))
            {
                throw new CountryCodeDoesNotExistException("Please check if the input alphabetic country codes exist or are available");
            }
        }

        private static bool IsCountryCodeExist(string[] codes)
        {
            var currencySet = XmlHelper.LoadAvailableCurrenciesFromXml();
            return currencySet.IsSupersetOf(codes);
        }

        private static string[] GetCountriesAlfabeticCode(this string[] args)
        {
            if (!TryParseCurrencyPair(args[0], out string[] exchange))
            {
                throw new AlphabeticCountryCodesException("Alphabetic country codes must contain 3 characters separated by '/' (e.g., 'xxx/yyy')");
            }
            exchange[2] = args[1];
            return exchange;
        }

        private static bool TryParseCurrencyPair(string input, out string[] parsedParameters)
        {
            const string pattern = @"^([a-zA-Z]{3})/([a-zA-Z]{3})$";

            var match = Regex.Match(input.ToUpper(), pattern, RegexOptions.Compiled);

            if (!match.Success)
            {
                throw new TryParseCurrencyPairException("Currency pair is not valid");
            }

            parsedParameters = [match.Groups[1].Value, match.Groups[2].Value, string.Empty];
            return true;
        }
    }
}
