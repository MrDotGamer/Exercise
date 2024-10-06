using Exchange.Services.Interfaces;

namespace Exchange.Services
{
    /// <summary>
    /// Represents the context for executing exchange strategies.
    /// </summary>
    public class ExchangeStrategyContext : IStrategyContext
    {
        private readonly Dictionary<string, IStrategy> _strategies = [];

        /// <summary>
        /// Adds a strategy to the context.
        /// </summary>
        /// <param name="key">The key associated with the strategy.</param>
        /// <param name="strategy">The strategy to add.</param>
        public void AddStrategy(string key, IStrategy strategy)
        {
            _strategies[key] = strategy;
        }

        /// <summary>
        /// Executes the strategy associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the strategy to execute.</param>
        /// <param name="args">The arguments to pass to the strategy.</param>
        /// <returns>A task representing the asynchronous execution of the strategy.</returns>
        public async Task ExecuteStrategyAsync(string key, object args)
        {
            if (_strategies.TryGetValue(key, out var value) && value is not null)
            {
                await value.ExecuteAsync(args);
            }
            else
            {
                throw new ArgumentException($"Strategy not found for key: {key}");
            }
        }
    }
}
