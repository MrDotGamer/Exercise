using FluentValidation;
using System.Text.RegularExpressions;

namespace Exchange.Services
{
    /// <summary>
    /// Validator for validating currency alphabetic country codes.
    /// </summary>
    public class CurrencyAlphabeticCountryCodeValidator : AbstractValidator<string[]>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyAlphabeticCountryCodeValidator"/> class.
        /// </summary>
        public CurrencyAlphabeticCountryCodeValidator()
        {
            RuleFor(args => args[0]).Must(TryParseCurrencyPair)
                .WithMessage("Alphabetic country codes must contain 3 characters separated by '/' (e.g., 'xxx/yyy')");
        }

        /// <summary>
        /// Tries to parse the currency pair from the input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns><c>true</c> if the currency pair is successfully parsed; otherwise, <c>false</c>.</returns>
        private bool TryParseCurrencyPair(string input)
        {
            const string pattern = @"^([a-zA-Z]{3})/([a-zA-Z]{3})$";
            return Regex.IsMatch(input, pattern);
        }
    }
}
