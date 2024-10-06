using Exchange.Handlers;
using Exchange.Services.Interfaces;
using System.Text;

namespace Exchange.Services
{
    public class CurrencyManager(IGetAvailableRates getAvailableRates, IXmlService xmlService) : IManager
    {
        private readonly IGetAvailableRates _getAvailableRates = getAvailableRates;
        private readonly IXmlService _xmlService = xmlService;

        public decimal Exchange(string currencyFrom, string currencyTo, decimal amount)
        {
            var list = _getAvailableRates.GetRates();
            var from = list[currencyFrom];
            var to = list[currencyTo];
            return from / to * amount;
        }

        public void PrintResult(string[] args, decimal amount)
        {
            StringBuilder builder = new();

            builder.AppendLine($"Exchanger change {args[2]} of {args[0]} to {amount} of {args[1]}");

            Console.WriteLine(builder.ToString());
        }

        public string[] ValidateArguments(string[] args)
        {
            var handler = new ValidateArgumentsHandler();
            handler.SetNext(new AlphabeticalCountryCodeHandler())
                   .SetNext(new CurrencyArgumentsHandler())
                   .SetNext(new CountryCodeHandler(_xmlService));

            return handler.Handle(args);
        }
    }
}
