namespace Application.Devices.Commands.DeleteDevice;

public class DeleteDeviceCommandValidator : AbstractValidator<DeleteDeviceCommand>
{
    public DeleteDeviceCommandValidator()
    {
        RuleFor(v => v.DeviceId)
            .NotEmpty();
    }
}