using CommandLine;

namespace Exchange.Models
{
    public class Options
    {
        [Option('e', "exchange", Required = false, HelpText = "Command example -e xxx/yyy 23")]
        public IEnumerable<string>? Exchange { get; set; }
    }
}
