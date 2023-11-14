namespace Application.Users.Commands.ReqUserPasswordChange;

public class ReqUserPasswordChangeCommandValidator : AbstractValidator<ReqUserPasswordChangeCommand>
{
    public ReqUserPasswordChangeCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
