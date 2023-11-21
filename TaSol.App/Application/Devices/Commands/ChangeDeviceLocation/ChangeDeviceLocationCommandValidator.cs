namespace Application.Devices.Commands.ChangeDeviceLocation;

public class ChangeDeviceLocationCommandValidator : AbstractValidator<ChangeDeviceLocationCommand>
{
    public ChangeDeviceLocationCommandValidator()
    {
        RuleFor(v => v.DeviceId)
            .NotEmpty();

        RuleFor(v => v.NewLocationId)
            .NotEmpty();
    }
}
