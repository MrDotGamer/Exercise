namespace Exchange.Services.Interfaces
{
    public interface IStrategyContext
    {
        void AddStrategy(string key, IStrategy strategy);
        Task ExecuteStrategyAsync(string key, object args);
    }
}
