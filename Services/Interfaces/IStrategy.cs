namespace Exchange.Services.Interfaces
{
    public interface IStrategy
    {
        string Name { get; }
        void Execute(object args);
    }
}
