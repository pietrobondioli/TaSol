namespace Application.EnvironmentInfos.Commands.CreateEnvironmentInfo;

public class CreateEnvironmentInfoCommandValidator : AbstractValidator<CreateEnvironmentInfoCommand>
{
    public CreateEnvironmentInfoCommandValidator()
    {
        RuleFor(v => v.Temperature)
            .NotNull();

        RuleFor(v => v.Humidity)
            .NotNull();

        RuleFor(v => v.LightLevel)
            .NotNull();

        RuleFor(v => v.RainLevel)
            .NotNull();

        RuleFor(v => v.AuthToken)
            .NotEmpty();
    }
}
