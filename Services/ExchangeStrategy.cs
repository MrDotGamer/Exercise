using Exchange.Services.Interfaces;

namespace Exchange.Services
{
    /// <summary>
    /// Represents an exchange strategy.
    /// </summary>
    public class ExchangeStrategy(IManager manager) : IStrategy
    {
        /// <summary>
        /// Gets the name of the exchange strategy.
        /// </summary>
        public string Name => "Exchange";

        private readonly IManager _manager = manager;

        /// <summary>
        /// Executes the exchange strategy asynchronously.
        /// </summary>
        /// <param name="args">The arguments for the exchange strategy.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ExecuteAsync(object args)
        {
            if (args is string[] stringArgs)
            {
                var validArguments = await _manager.ValidateArgumentsAsync(stringArgs[1..3]);

                var amount = await _manager.ExchangeAsync(validArguments[0], validArguments[1], decimal.Parse(validArguments[2]));

                await _manager.PrintResultAsync(validArguments, amount);
            }
            else
            {
                throw new ArgumentException("Invalid arguments");
            }
        }
    }
}
