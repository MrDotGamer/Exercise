using Exchange.Services.Interfaces;

namespace Exchange.Extensions
{
    public static class AddStrategiesExtension
    {
        public static void AddStrategies(this IStrategyContext context, IEnumerable<IStrategy> strategies)
        {
            foreach (var strategy in strategies)
            {
                context.AddStrategy(strategy.Name, strategy);
            }
        }
    }
}
