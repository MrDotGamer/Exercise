using System.Xml.Linq;

namespace Exchange.Utilities
{
    public static class XmlHelper
    {
        public static HashSet<string> LoadAvailableCurrenciesFromXml(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            return new HashSet<string>(doc.Descendants("CcyNtry")
                      .Select(x => x.Element("Ccy")?.Value)
                      .Where(value => value != null)!);
        }
    }
}
