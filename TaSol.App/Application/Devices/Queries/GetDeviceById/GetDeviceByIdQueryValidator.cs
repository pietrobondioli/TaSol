namespace Application.Queries.Queries.GetDeviceById;

public class GetDeviceByIdQueryValidator : AbstractValidator<GetDeviceByIdQuery>
{
    public GetDeviceByIdQueryValidator()
    {
        RuleFor(v => v.DeviceId)
            .NotEmpty().WithMessage("DeviceId is required.")
            .NotNull();
    }
}
