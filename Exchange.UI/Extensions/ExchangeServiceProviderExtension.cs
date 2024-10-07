using Exchange.Application.Services;
using Exchange.Core.Data.Repository;
using Exchange.Core.Services;
using Exchange.Data.DataServices;
using Exchange.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace Exchange.UI.Extensions
{
    public static class ExchangeServiceProviderExtension
    {
        public static ServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddTransient<IGetAvailableRates, GetAvailableRatesHardcoded>()
                .AddTransient<IManager, CurrencyManager>()
                .AddTransient<ICheckCountryCodeService, XmlCountryCodeService>()
                .AddTransient<IStrategy, ExchangeStrategy>()
                .AddTransient<IStrategyContext, ExchangeStrategyContext>()
                .BuildServiceProvider();
        }
    }
}
