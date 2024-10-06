namespace Exchange.Services.Interfaces
{
    public interface IManager
    {
        Task<string[]> ValidateArgumentsAsync(string[] args);
        Task<decimal> ExchangeAsync(string currencyFrom, string currencyTo, decimal amount);
        Task PrintResultAsync(string[] args, decimal result);
    }
}