namespace Exchange.Services.Interfaces
{
    /// <summary>
    /// Represents a strategy context that manages multiple strategies.
    /// </summary>
    public interface IStrategyContext
    {
        /// <summary>
        /// Adds a strategy to the context.
        /// </summary>
        /// <param name="key">The key to associate with the strategy.</param>
        /// <param name="strategy">The strategy to add.</param>
        void AddStrategy(string key, IStrategy strategy);

        /// <summary>
        /// Executes a strategy asynchronously.
        /// </summary>
        /// <param name="key">The key associated with the strategy.</param>
        /// <param name="args">The arguments to pass to the strategy.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ExecuteStrategyAsync(string key, object args);
    }
}
