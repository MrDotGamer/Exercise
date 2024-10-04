namespace Exchange.Services.Interfaces
{
    public interface IManager
    {
        decimal Exchange(string currencyFrom, string currencyTo, decimal amount);
        void PrintResult(string[] args, decimal result);
    }
}