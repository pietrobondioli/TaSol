namespace Application.Commands.Commands.RegenerateAuthToken;

public class RegenerateAuthTokenCommandValidator : AbstractValidator<RegenerateAuthTokenCommand>
{
    public RegenerateAuthTokenCommandValidator()
    {
        RuleFor(v => v.DeviceId)
            .NotEmpty();
    }
}