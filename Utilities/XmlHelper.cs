using System.Xml.Linq;

namespace Exchange.Utilities
{
    public static class XmlHelper
    {
        /// <summary>
        /// Loads the available currencies from an XML file.
        /// </summary>
        /// <param name="filePath">The path to the XML file.</param>
        /// <returns>A HashSet containing the available currencies.</returns>
        public static HashSet<string> LoadAvailableCurrenciesFromXml(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            return new HashSet<string>(doc.Descendants("CcyNtry")
                      .Select(x => x.Element("Ccy")?.Value)
                      .Where(value => value != null)!);
        }
    }
}
