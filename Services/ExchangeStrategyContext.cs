using Exchange.Services.Interfaces;

namespace Exchange.Services
{
    public class ExchangeStrategyContext : IStrategyContext
    {
        private readonly Dictionary<string, IStrategy> _strategies = [];

        public void AddStrategy(string key, IStrategy strategy)
        {
            _strategies[key] = strategy;
        }

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
