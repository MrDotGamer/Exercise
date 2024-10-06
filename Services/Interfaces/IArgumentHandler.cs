namespace Exchange.Services.Interfaces
{
    /// <summary>
    /// Represents an interface for handling command line arguments.
    /// </summary>
    public interface IArgumentHandler
    {
        /// <summary>
        /// Handles the command line arguments asynchronously.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>An array of strings representing the result of handling the arguments.</returns>
        Task<string[]> HandleAsync(string[] args);

        /// <summary>
        /// Sets the next argument handler in the chain of responsibility.
        /// </summary>
        /// <param name="handler">The next argument handler.</param>
        /// <returns>The next argument handler.</returns>
        IArgumentHandler SetNext(IArgumentHandler handler);
    }
}