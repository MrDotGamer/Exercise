using FluentValidation;

namespace Exchange.Application.Validators
{

    /// <summary>
    /// Validator for validating currency amount arguments.
    /// Possible formats:
    /// •	123
    /// •	123.45
    /// •	123,45
    /// •	.45
    /// •	,45
    /// •	123.
    /// •	123,
    /// </summary>
    public class CurrencyAmountArgsValidator : AbstractValidator<string[]>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyAmountArgsValidator"/> class.
        /// </summary>
        public CurrencyAmountArgsValidator()
        {
            RuleFor(args => args[2]).Matches(@"^(\d+([,.]\d{0,2})?|[,.]\d{1,2})$");
        }
    }
}
