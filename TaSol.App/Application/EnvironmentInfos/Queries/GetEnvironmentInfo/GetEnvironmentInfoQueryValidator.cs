namespace Application.EnvironmentInfos.Queries.GetEnvironmentInfo;

public class GetEnvironmentInfoQueryValidator : AbstractValidator<GetEnvironmentInfoQuery>
{
    public GetEnvironmentInfoQueryValidator()
    {
        RuleFor(x => x.LocationId).NotEmpty();
        RuleFor(x => x.Range).IsInEnum();
        RuleFor(x => x.Interval).IsInEnum();
    }
}
