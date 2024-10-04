using FluentValidation;

namespace Exchange.Services
{
    public class ArgumentsValidator : AbstractValidator<string[]>
    {
        public ArgumentsValidator()
        {
            RuleFor(args => args.Length).Equal(2)
                .WithMessage("Exactly 2 arguments are required, command example: xxx/yyy amount");
        }
    }
}
