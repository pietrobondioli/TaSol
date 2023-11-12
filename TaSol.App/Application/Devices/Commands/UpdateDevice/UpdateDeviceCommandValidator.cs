namespace Application.Devices.Commands.UpdateDevice;

public class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceCommandValidator()
    {
        RuleFor(v => v.DeviceId)
            .NotEmpty();

        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Description)
            .MaximumLength(1000)
            .NotEmpty();
    }
}