namespace Application.Users.Commands.ReqNewUserVerificationToken;

public class ReqNewUserVerificationTokenCommandValidator : AbstractValidator<ReqNewUserVerificationTokenCommand>
{
    public ReqNewUserVerificationTokenCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
