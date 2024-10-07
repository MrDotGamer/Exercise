using Exchange.Services.Interfaces;

namespace Exchange.Application.Services
{
    /// <summary>
    /// Represents an abstract base class for handling arguments.
    /// </summary>
    public abstract class ArgumentHandlerService : IArgumentHandler
    {
        private IArgumentHandler? _nextHandler;

        /// <summary>
        /// Sets the next argument handler in the chain.
        /// </summary>
        /// <param name="handler">The next argument handler.</param>
        /// <returns>The next argument handler.</returns>
        public IArgumentHandler SetNext(IArgumentHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        /// <summary>
        /// Handles the arguments asynchronously.
        /// </summary>
        /// <param name="args">The arguments to handle.</param>
        /// <returns>The handled arguments.</returns>
        public virtual async Task<string[]> HandleAsync(string[] args)
        {
            if (_nextHandler != null)
            {
                return await _nextHandler.HandleAsync(args);
            }
            return args;
        }
    }
}