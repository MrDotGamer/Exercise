using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace Exchange
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var checkedArguments = args.CheckArguments();

                var serviceProvider = ExchangeServiceProvider.BuildServiceProvider();

                var exchangeStrategy = serviceProvider.GetService<IManager>() ?? throw new CurrencyServiceException("Currency service Not Found");

                var amount = exchangeStrategy.Exchange(checkedArguments[0], checkedArguments[1], decimal.Parse(checkedArguments[2]));

                PrintResult(args, amount);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void PrintResult(string[] args, decimal amount)
        {
            StringBuilder builder = new();

            builder.AppendLine("The following arguments are passed:");

            foreach (var arg in args)
            {
                builder.AppendLine($"Argument={arg}");
            }
            Console.WriteLine($"Amount of money {amount}");
            Console.WriteLine(builder.ToString());
        }
    }
}
/// <summary>
/// How often currecy list should be updated?????????
/// </summary>
public static class Extension
{
    static readonly string xmlFilePath = ConfigurationManager.AppSettings["FilePath"]!;

    public static string[] CheckArguments(this string[] args)
    {
        return args.IsValidArgumentCount()
                            .CountriesAlfabeticCodeIsValid()
                            .AreExsistingCountryCodes()
                            .TryParseCurrency();
    }
    private static string[] IsValidArgumentCount(this string[] args)
    {
        if (args.Length != 2)
        {
            if (args.Length < 2)
                throw new ArgumentCountException("Some arguments are missed");
            throw new ArgumentCountException("Too many arguments are passed");
        }

        return args;
    }

    private static bool IsSuccessfullyParced(this string[] args)
    {
        if (args.Length != 3)
        {
            return false;
        }
        return true;
    }

    private static string[] TryParseCurrency(this string[] args)
    {
        if (args.IsArrayEmpty())
        {
            throw new EmptyArrayException("No arguments passed");
        }

        const string pattern = @"^(\d+([,.]\d{0,2})?|[,.]\d{1,2})$";
        if (!Regex.IsMatch(args[2], pattern))
        {
            throw new BadMoneyFormatException("Money format is not valid");
        }
        args[2] = args[2].Replace(",", ".");
        return args;
    }
    private static bool IsArrayEmpty(this string[] args)
    {
        return args.Length == 0;
    }
    private static string[] AreExsistingCountryCodes(this string[] args)
    {
        if (!IsCountryCodeExist(args[..2]))
        {
            throw new CountryCodeDoesNotExistException("Please chek input alphabetic country code does not exist or available");
        }
        return args;
    }
    private static bool IsCountryCodeExist(string[] codes)
    {

        var currencySet = LoadAvailableCurrenciesFromXml(xmlFilePath);

        return currencySet.IsSupersetOf(codes);
    }
    private static HashSet<string> LoadAvailableCurrenciesFromXml(string filePath)
    {
        XDocument doc = XDocument.Load(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, xmlFilePath));
        return new HashSet<string>(doc.Descendants("CcyNtry")
                  .Select(x => x.Element("Ccy")?.Value)
                  .Where(value => value != null)!);
    }

    private static string[] CountriesAlfabeticCodeIsValid(this string[] args)
    {
        if (!TryParseCurrencyPair(args[0], out string[] exchange))
        {
            throw new AlphabeticCountryCodesException("Alfabetic country codes must contains 3 chars splitted by '/' as example: 'xxx/yyy'");
        }
        exchange[2] = args[1];
        return exchange;
    }

    private static bool TryParseCurrencyPair(string input, out string[] parsedParameters)
    {
        const string pattern = @"^([a-zA-Z]{3})/([a-zA-Z]{3})$";

        var match = Regex.Match(input.ToUpper(), pattern, RegexOptions.Compiled);

        if (!match.Success)
        {
            parsedParameters = [];
            return false;

        }

        parsedParameters = [match.Groups[1].Value, match.Groups[2].Value, string.Empty];
        return true;
    }
}


public class CurrencyManager : IManager
{
    private readonly IGetAvalableRates _getAvalableRates;
    public CurrencyManager(IGetAvalableRates getAvalableRates)
    {
        _getAvalableRates = getAvalableRates;
    }
    public decimal Exchange(string currencyFrom, string currencyTo, decimal amount)
    {
        var list = _getAvalableRates.GetRates();
        var from = list.First(x => x.Key == currencyFrom).Value;
        var to = list.First(x => x.Key == currencyTo).Value;
        return (from / to) * amount;
    }

}
public interface IManager
{
    public decimal Exchange(string currencyFrom, string currencyTo, decimal amount);

}

public interface IGetAvalableRates
{
    public KeyValuePair<string, decimal>[] GetRates();
}

/// <summary>
/// This must get real time information 
/// </summary>
public class GetAvalableRates : IGetAvalableRates
{
    public KeyValuePair<string, decimal>[] GetRates()
    {
        return
        [
            new("EUR", 7.4394M),
            new("USD", 6.6311M),
            new("GBP", 8.5285M),
            new("SEK", 0.7610M),
            new("NOK", 0.7840M),
            new("CHF", 6.8358M),
            new("JPY", 0.059740M),
            new("DKK", 1M)
        ];
    }
}
public class EmptyArrayException : Exception
{
    public EmptyArrayException(string message) : base(message)
    {
    }
}

public class ArgumentCountException : Exception
{
    public ArgumentCountException(string message) : base(message)
    {
    }
}

public class BadMoneyFormatException : Exception
{
    public BadMoneyFormatException(string message) : base(message)
    {
    }
}

public class AlphabeticCountryCodesException : Exception
{
    public AlphabeticCountryCodesException(string message) : base(message)
    {
    }
}

public class CountryCodeDoesNotExistException : Exception
{
    public CountryCodeDoesNotExistException(string message) : base(message)
    {
    }
}

public class CurrencyServiceException : Exception
{
    public CurrencyServiceException(string message) : base(message)
    {
    }
}