using Microsoft.Extensions.DependencyInjection;

namespace Exchange
{
    public static class ExchangeServiceProvider
    {
        public static ServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddTransient<IGetAvalableRates, GetAvalableRates>()
                .AddTransient<IManager, CurrencyManager>()
                .BuildServiceProvider();
        }
    }
}
