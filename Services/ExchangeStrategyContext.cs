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

        public void ExecuteStrategy(string key, object args)
        {
            if (_strategies.TryGetValue(key, out var value) && value is not null)
            {
                value.Execute(args);
            }
            else
            {
                throw new ArgumentException($"Strategy not found for key: {key}");
            }
        }
    }
}
