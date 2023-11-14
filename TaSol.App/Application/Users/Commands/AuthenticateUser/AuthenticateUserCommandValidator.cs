namespace Application.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .When(x => string.IsNullOrEmpty(x.UserName));

        RuleFor(x => x.UserName)
            .NotEmpty()
            .When(x => string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
