using Exchange.Services.Interfaces;

namespace Exchange.Services
{
    public class ExchangeStrategy(IManager manager) : IStrategy
    {
        public string Name => "Exchange";
        private readonly IManager _manager = manager;

        public void Execute(object args)
        {
            if (args is string[] stringArgs)
            {
                var validArguments = _manager.ValidateArguments(stringArgs[1..3]);

                var amount = _manager.Exchange(validArguments[0], validArguments[1], decimal.Parse(validArguments[2]));

                _manager.PrintResult(validArguments, amount);
            }
            else
            {
                throw new ArgumentException("Invalid arguments");
            }

        }
    }
}
