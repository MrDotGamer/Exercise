using CommandLine;

namespace Exchange.UI.Data.Model
{
    /// <summary>
    /// Represents the options which strategy will run.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the exchange arguments.
        /// </summary>
        [Option('e', "exchange", Required = false, HelpText = "Command example: -e xxx/yyy 23")]
        public IEnumerable<string>? Exchange { get; set; }
    }
}
