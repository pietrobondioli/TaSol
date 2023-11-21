namespace Application.Devices.Commands.EnableDevice;

public class EnableDeviceCommandValidator : AbstractValidator<EnableDeviceCommand>
{
    public EnableDeviceCommandValidator()
    {
        RuleFor(v => v.DeviceId)
            .NotEmpty();
    }
}
