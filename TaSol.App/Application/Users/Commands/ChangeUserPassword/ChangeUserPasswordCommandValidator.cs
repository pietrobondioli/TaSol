using Application.Common.Validators;

namespace Application.Users.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();

        RuleFor(x => x.NewPassword).NotEmpty().StrongPassword();

        RuleFor(x => x.ConfirmNewPassword).NotEmpty().PasswordConfirmation(x => x.NewPassword);
    }
}
