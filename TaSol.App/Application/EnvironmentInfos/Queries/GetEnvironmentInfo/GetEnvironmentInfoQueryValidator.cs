using Application.EnvironmentInfos.Constants;

namespace Application.Queries.Queries.GetEnvironmentInfo;

public class GetEnvironmentInfoQueryValidator : AbstractValidator<GetEnvironmentInfoQuery>
{
    public GetEnvironmentInfoQueryValidator()
    {
        RuleFor(x => x.LocationId).NotEmpty();
        RuleFor(x => x.Range).IsInEnum();
        RuleFor(x => x.Interval).IsInEnum();
    }
}
