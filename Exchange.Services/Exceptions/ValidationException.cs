using FluentValidation.Results;

namespace Exchange.Application.Exceptions
{
    public class ValidationException(List<ValidationFailure> errors) : Exception("Parameters validation exception occurred. Example xxx/yyy 20")
    {
        public List<ValidationFailure> Errors { get; } = errors;
    }
}
