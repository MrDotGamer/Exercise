using CommandLine;
using Exchange.Services.Interfaces;
using Exchange.UI.Data.Model;
using Exchange.UI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Exchange.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var serviceProvider = ExchangeServiceProviderExtension.BuildServiceProvider();
                var strategies = serviceProvider.GetServices<IStrategy>() ?? [];

                var context = serviceProvider.GetService<IStrategyContext>() ?? throw new ArgumentNullException("Strategy context not found");

                context.AddStrategies(strategies);

                await Parser.Default.ParseArguments<Options>(args)
                .WithParsedAsync(async o =>
                {
                    var options = o.GetType()
                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                     .ToDictionary(prop => prop.Name, prop => prop.GetValue(o, null));
                    foreach (var (key, value) in options)
                    {
                        if (value is not null)
                        {
                            await context.ExecuteStrategyAsync(key, args);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
