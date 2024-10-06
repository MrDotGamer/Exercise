namespace Exchange.Services.Interfaces
{
    public interface IStrategy
    {
        string Name { get; }
        Task ExecuteAsync(object args);
    }
}
