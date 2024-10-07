using FluentValidation.Results;

namespace Exchange.Application.Exceptions
{
    public class ValidateCurrencyCountryCodeArgumentsException(List<ValidationFailure> errors) : Exception("Currency alphabetic country code validation exception occurred. Supported format xxx/yyy")
    {
        public List<ValidationFailure> Errors { get; } = errors;
    }
}
