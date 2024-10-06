using Exchange.Services;

namespace Exchange.Handlers
{
    /// <summary>
    /// Represents a handler for validating arguments.
    /// </summary>
    public class ValidateArgumentsHandler() : ArgumentHandler
    {
        private readonly ArgumentsValidator _argumentsValidator = new();

        /// <summary>
        /// Handles the validation of arguments.
        /// </summary>
        /// <param name="args">The arguments to validate.</param>
        /// <returns>A task representing the asynchronous operation. The result is an array of strings.</returns>
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