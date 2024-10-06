using Exchange.Exceptions;
using Exchange.Services;

namespace Exchange.Handlers
{
    /// <summary>
    /// Handles the validation and processing of alphabetical country codes.
    /// </summary>
    public class AlphabeticalCountryCodeHandler() : ArgumentHandler
    {
        private readonly CurrencyAlphabeticCountryCodeValidator _alphabeticCountryCodeValidator = new();

        /// <summary>
        /// Handles the asynchronous processing of the provided arguments.
        /// </summary>
        /// <param name="args">The arguments to be processed.</param>
        /// <returns>An array of strings representing the processed arguments.</returns>
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
