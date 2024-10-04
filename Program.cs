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
                var serviceProvider = ExchangeServiceProviderExtension.BuildServiceProvider();

                var currencyService = serviceProvider.GetService<IManager>() ?? throw new CurrencyServiceException("Currency service Not Found");

                var validArguments = currencyService.ValidateArguments(args);

                var amount = currencyService.Exchange(validArguments[0], validArguments[1], decimal.Parse(validArguments[2]));

                currencyService.PrintResult(validArguments, amount);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
