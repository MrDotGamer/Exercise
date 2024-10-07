using FluentValidation;

namespace Exchange.Application.Validators
{
    /// <summary>
    /// Validates the arguments passed to a command.
    /// </summary>
    public class ArgumentsValidator : AbstractValidator<string[]>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsValidator"/> class.
        /// </summary>
        public ArgumentsValidator()
        {
            RuleFor((args) => args.Length).Equal(2)
                 .WithMessage("2 arguments are required, command example: xxx/yyy 123(amount of money)");
        }
    }
}
