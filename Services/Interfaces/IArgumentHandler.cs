namespace Exchange.Services.Interfaces
{
    public interface IArgumentHandler
    {
        Task<string[]> HandleAsync(string[] args);
        IArgumentHandler SetNext(IArgumentHandler handler);
    }
}