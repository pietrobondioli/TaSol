namespace Application.Commands.Commands.ReactivateDevice;

public class ReactivateDeviceCommandValidator : AbstractValidator<ReactivateDeviceCommand>
{
    public ReactivateDeviceCommandValidator()
    {
        RuleFor(v => v.DeviceId)
            .NotEmpty();
    }
}