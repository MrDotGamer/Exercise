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
        public bool CheckCountryCode(string[] codes)
        {
            var currencySet = LoadCountryCodes();
            return currencySet.IsSupersetOf(codes);
        }
        private HashSet<string> LoadCountryCodes()
        {
            return XmlHelper.LoadAvailableCurrenciesFromXml(filePath);
        }
    }
}
