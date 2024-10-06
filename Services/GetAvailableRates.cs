using Exchange.Services.Interfaces;

namespace Exchange.Services
{
    /// <summary>
    /// Represents a service for retrieving available rates.
    /// </summary>
    public class GetAvailableRates : IGetAvailableRates
    {
        /// <summary>
        /// Retrieves the available rates asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a dictionary of rates, where the key is the currency code and the value is the exchange rate.</returns>
        public Task<Dictionary<string, decimal>> GetRatesAsync()
        {
            return Task.FromResult(new Dictionary<string, decimal>
                {
                    { "EUR", 7.4394M },
                    { "USD", 6.6311M },
                    { "GBP", 8.5285M },
                    { "SEK", 0.7610M },
                    { "NOK", 0.7840M },
                    { "CHF", 6.8358M },
                    { "JPY", 0.059740M },
                    { "DKK", 1M }
                });
        }
    }
}
