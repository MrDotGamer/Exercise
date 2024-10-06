using Exchange.Services.Interfaces;

namespace Exchange.Services
{
    public class ExchangeStrategy(IManager manager) : IStrategy
    {
        public string Name => "Exchange";
        private readonly IManager _manager = manager;

        public async Task ExecuteAsync(object args)
        {
            if (args is string[] stringArgs)
            {
                var validArguments = await _manager.ValidateArgumentsAsync(stringArgs[1..3]);

                var amount = await _manager.ExchangeAsync(validArguments[0], validArguments[1], decimal.Parse(validArguments[2]));

                await _manager.PrintResultAsync(validArguments, amount);
            }
            else
            {
                throw new ArgumentException("Invalid arguments");
            }

        }
    }
}
