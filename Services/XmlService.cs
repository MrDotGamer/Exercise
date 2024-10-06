using Exchange.Services.Interfaces;
using Exchange.Utilities;
using System.Configuration;

namespace Exchange.Services
{
    public class XmlCountryCodeService : IXmlService
    {
        readonly string filePath;
        public XmlCountryCodeService()
        {
            filePath = Path.Combine(AppContext.BaseDirectory, ConfigurationManager.AppSettings["AvailableCurrenciesFilePath"]!);

        }
        public async Task<bool> CheckCountryCodeAsync(string[] codes)
        {
            var currencySet = await Task.Run(() => LoadCountryCodes());
            return currencySet.IsSupersetOf(codes);
        }
        private HashSet<string> LoadCountryCodes()
        {
            return XmlHelper.LoadAvailableCurrenciesFromXml(filePath);
        }
    }
}
