using Exchange.Services.Interfaces;

namespace Exchange.Services
{
    public abstract class ArgumentHandler : IArgumentHandler
    {
        private IArgumentHandler? _nextHandler;

        public IArgumentHandler SetNext(IArgumentHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

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