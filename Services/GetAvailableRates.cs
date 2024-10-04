namespace Exchange.Services
{
    public class GetAvailableRates : IGetAvailableRates
    {
        public Dictionary<string, decimal> GetRates()
        {
            return new Dictionary<string, decimal>
            {
                { "EUR", 7.4394M },
                { "USD", 6.6311M },
                { "GBP", 8.5285M },
                { "SEK", 0.7610M },
                { "NOK", 0.7840M },
                { "CHF", 6.8358M },
                { "JPY", 0.059740M },
                { "DKK", 1M }
            };
        }
    }
}
