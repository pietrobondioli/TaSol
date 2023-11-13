namespace Application.Devices.Commands.CreateDevice;

public class CreateDeviceCommandValidator : AbstractValidator<CreateDeviceCommand>
{
    public CreateDeviceCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Description)
            .MaximumLength(1000)
            .NotEmpty();

        RuleFor(v => v.LocationId)
            .NotNull();
    }
}
