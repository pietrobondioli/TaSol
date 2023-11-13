namespace Application.Devices.Commands.DisableDevice;

public class DisableDeviceCommandValidator : AbstractValidator<DisableDeviceCommand>
{
    public DisableDeviceCommandValidator()
    {
        RuleFor(v => v.DeviceId)
            .NotEmpty();
    }
}
