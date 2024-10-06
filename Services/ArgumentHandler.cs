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

        public virtual string[] Handle(string[] args)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(args);
            }
            return args;
        }
    }
}