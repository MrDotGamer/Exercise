namespace Exchange.Services.Interfaces
{
    public interface IStrategyContext
    {
        void AddStrategy(string key, IStrategy strategy);
        void ExecuteStrategy(string key, object args);
    }
}
