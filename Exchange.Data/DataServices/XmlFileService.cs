﻿using Exchange.Services.Interfaces;
using System.Configuration;
using System.Xml.Linq;

namespace Exchange.Data.DataServices
{
    /// <summary>
    /// Represents a service for checking country codes using XML data.
    /// </summary>
    public class XmlCountryCodeService : ICheckCountryCodeService
    {
        readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlCountryCodeService"/> class.
        /// </summary>
        public XmlCountryCodeService()
        {
            filePath = Path.Combine(AppContext.BaseDirectory, ConfigurationManager.AppSettings["AvailableCurrenciesFilePath"]!);
        }

        /// <summary>
        /// Asynchronously checks if the given country codes are valid.
        /// </summary>
        /// <param name="codes">The country codes to check.</param>
        /// <returns><c>true</c> if all the country codes are valid; otherwise, <c>false</c>.</returns>
        public async Task<bool> CheckCountryCodeAsync(string[] codes)
        {
            var currencySet = await Task.Run(() => LoadCountryCodes());
            return currencySet.IsSupersetOf(codes);
        }

        private HashSet<string> LoadCountryCodes()
        {
            XDocument doc = XDocument.Load(filePath);
            return new HashSet<string>(doc.Descendants("CcyNtry")
                      .Select(x => x.Element("Ccy")?.Value)
                      .Where(value => value != null)!);
        }
    }
}
