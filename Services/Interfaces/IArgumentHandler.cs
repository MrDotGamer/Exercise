namespace Exchange.Services.Interfaces
{
    public interface IArgumentHandler
    {
        string[] Handle(string[] args);
        IArgumentHandler SetNext(IArgumentHandler handler);
    }
}