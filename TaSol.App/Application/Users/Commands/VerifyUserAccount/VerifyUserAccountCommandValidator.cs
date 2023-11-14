namespace Application.Users.Commands.VerifyUserAccount;

public class VerifyUserAccountCommandValidator : AbstractValidator<VerifyUserAccountCommand>
{
    public VerifyUserAccountCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}
