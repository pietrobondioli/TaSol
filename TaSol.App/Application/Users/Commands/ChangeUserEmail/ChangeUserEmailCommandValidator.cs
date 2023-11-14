namespace Application.Users.Commands.ChangeUserEmail;

public class ChangeUserEmailCommandValidator : AbstractValidator<ChangeUserEmailCommand>
{
    public ChangeUserEmailCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();

        RuleFor(x => x.NewEmail)
            .NotEmpty()
            .EmailAddress();
    }
}
