namespace Exchange.Services.Interfaces
{
    public interface IGetAvailableRates
    {
        Dictionary<string, decimal> GetRates();
    }
}