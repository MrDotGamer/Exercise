namespace Exchange.Services.Interfaces
{
    /// <summary>
    /// Represents a strategy interface.
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// Gets the name of the strategy.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Executes the strategy asynchronously.
        /// </summary>
        /// <param name="args">The arguments for the strategy execution.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ExecuteAsync(object args);
    }
}
