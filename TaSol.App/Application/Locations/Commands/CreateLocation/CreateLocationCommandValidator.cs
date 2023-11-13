namespace Application.Locations.Commands.CreateLocation;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(400);

        RuleFor(v => v.Address)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(v => v.City)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.State)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Country)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Latitude)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Longitude)
            .NotEmpty()
            .MaximumLength(200);
    }
}
