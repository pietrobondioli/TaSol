using System.Linq.Expressions;

namespace Application.Common.Validators;

public static class ValidatorsExtensions
{
    public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least 1 digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least 1 non alphanumeric character.");

        return options;
    }

    public static IRuleBuilderOptions<T, string> PasswordConfirmation<T>(this IRuleBuilder<T, string> ruleBuilder, Expression<Func<T, string>> password)
    {
        var options = ruleBuilder
            .NotEmpty()
            .Equal(password).WithMessage("Password and confirmation password must match.");

        return options;
    }
}
