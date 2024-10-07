using FluentValidation.Results;

namespace Exchange.Application.Exceptions
{
    public class ParseCurrencyArgumentsException(List<ValidationFailure> errors) : Exception("Currency validation exception occurred. Please check input amount, working formats(123; 123.45; .45; 123.), comma supported too")
    {
        public List<ValidationFailure> Errors { get; } = errors;
    }
}
