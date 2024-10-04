using FluentValidation;

namespace Exchange.Services
{

    public class CurrencyAmountArgsValidator : AbstractValidator<string[]>
    {
        public CurrencyAmountArgsValidator()
        {
            RuleFor(args => args[2]).Matches(@"^(\d+([,.]\d{0,2})?|[,.]\d{1,2})$");
        }
    }
}
