using Exchange.Exceptions;
using Exchange.Services;

namespace Exchange.Handlers
{
    /// <summary>
    /// Handles currency arguments for the exchange process.
    /// </summary>
    public class CurrencyArgumentsHandler() : ArgumentHandler
    {
        private readonly CurrencyAmountArgsValidator _currencyAmountValidator = new();

        /// <summary>
        /// Handles the currency arguments asynchronously.
        /// </summary>
        /// <param name="args">The currency arguments.</param>
        /// <returns>An array of strings.</returns>
        public override async Task<string[]> HandleAsync(string[] args)
        {
            var result = _currencyAmountValidator.Validate(args);
            if (!result.IsValid)
            {
                throw new ParseCurrencyArgumentsException(result.Errors);
            }

            args[2] = args[2].Replace(",", ".").TrimEnd('.', ',');
            return await base.HandleAsync(args);
        }
    }

}
