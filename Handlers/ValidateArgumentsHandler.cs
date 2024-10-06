using Exchange.Services;

namespace Exchange.Handlers
{
    public class ValidateArgumentsHandler() : ArgumentHandler
    {
        private readonly ArgumentsValidator _argumentsValidator = new();
        public override async Task<string[]> HandleAsync(string[] args)
        {
            var result = _argumentsValidator.Validate(args);
            if (!result.IsValid)
            {
                throw new FluentValidation.ValidationException(result.Errors);
            }
            return await base.HandleAsync(args);
        }
    }
}