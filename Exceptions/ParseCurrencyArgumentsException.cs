using FluentValidation.Results;

namespace Exchange.Exceptions
{
    public class ParseCurrencyArgumentsException(List<ValidationFailure> errors) : Exception("Currency validation exception occurred. Please check input amount, working formats(.2; 0.2; 20.03), comma supported")
    {
        public List<ValidationFailure> Errors { get; } = errors;
    }
}
