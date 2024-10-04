using Exchange.Services;
using Exchange.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace Exchange.Extensions
{
    public static class ExchangeServiceProviderExtension
    {
        public static ServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddTransient<IGetAvailableRates, GetAvailableRates>()
                .AddTransient<IManager, CurrencyManager>()
                .BuildServiceProvider();
        }
    }
}
