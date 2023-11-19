namespace Application.Queries.Queries.GetLocationById;

public class GetLocationByIdQueryValidator : AbstractValidator<GetLocationByIdQuery>
{
    public GetLocationByIdQueryValidator()
    {
        RuleFor(x => x.LocationId)
            .NotNull().WithMessage("LocationId should not be null");
    }
}
