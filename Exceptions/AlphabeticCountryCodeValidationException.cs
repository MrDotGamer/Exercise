using FluentValidation.Results;

namespace Exchange.Exceptions
{
    public class AlphabeticCountryCodeValidationException(List<ValidationFailure> errors) : Exception("Alphabetic country code validation exception occurred. Please check input country codes, example: xxx/yyy")
    {
        public List<ValidationFailure> Errors { get; } = errors;
    }
}
