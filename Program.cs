using Exchange.Exceptions;
using Exchange.Extensions;
using Exchange.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Exchange
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var checkedArguments = args.CheckArgumentsForExchangeService();

                var serviceProvider = ExchangeServiceProviderExtension.BuildServiceProvider();

                var currencyService = serviceProvider.GetService<IManager>() ?? throw new CurrencyServiceException("Currency service Not Found");

                var amount = currencyService.Exchange(checkedArguments[0], checkedArguments[1], decimal.Parse(checkedArguments[2]));

                currencyService.PrintResult(checkedArguments, amount);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
