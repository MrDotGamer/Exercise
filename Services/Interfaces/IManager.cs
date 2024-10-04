namespace Exchange.Services.Interfaces
{
    public interface IManager
    {
        string[] ValidateArguments(string[] args);
        decimal Exchange(string currencyFrom, string currencyTo, decimal amount);
        void PrintResult(string[] args, decimal result);
    }
}