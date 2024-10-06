namespace Exchange.Services.Interfaces
{
    /// <summary>
    /// Represents an interface for getting available rates.
    /// </summary>
    public interface IGetAvailableRates
    {
        /// <summary>
        /// Asynchronously retrieves the available rates.
        /// </summary>
        /// <returns>A dictionary containing the rates with currency codes as keys and decimal values.</returns>
        Task<Dictionary<string, decimal>> GetRatesAsync();
    }
}