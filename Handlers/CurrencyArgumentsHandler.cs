using Exchange.Exceptions;
using Exchange.Services;

namespace Exchange.Handlers
{
    public class CurrencyArgumentsHandler() : ArgumentHandler
    {
        private readonly CurrencyAmountArgsValidator _currencyAmountValidator = new();
        public override async Task<string[]> HandleAsync(string[] args)
        {
            var result = _currencyAmountValidator.Validate(args);
            if (!result.IsValid)
            {
                throw new ParseCurrencyArgumentsException(result.Errors);
            }

            args[2] = args[2].Replace(",", ".");
            return await base.HandleAsync(args);
        }
    }

}
