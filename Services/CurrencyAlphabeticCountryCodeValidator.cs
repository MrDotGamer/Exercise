using FluentValidation;
using System.Text.RegularExpressions;

namespace Exchange.Services
{
    public class CurrencyAlphabeticCountryCodeValidator : AbstractValidator<string[]>
    {
        public CurrencyAlphabeticCountryCodeValidator()
        {
            RuleFor(args => args[0]).Must(TryParseCurrencyPair)
                .WithMessage("Alphabetic country codes must contain 3 characters separated by '/' (e.g., 'xxx/yyy')");
        }
        private bool TryParseCurrencyPair(string input)
        {
            const string pattern = @"^([a-zA-Z]{3})/([a-zA-Z]{3})$";
            return Regex.IsMatch(input, pattern);
        }
    }
}
