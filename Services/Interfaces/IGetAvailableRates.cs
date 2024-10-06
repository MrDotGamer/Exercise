namespace Exchange.Services.Interfaces
{
    public interface IGetAvailableRates
    {
        Task<Dictionary<string, decimal>> GetRatesAsync();
    }
}