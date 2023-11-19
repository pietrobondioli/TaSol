namespace Application.Queries.Queries.GetLocationsWithPagination;

public class GetLocationsWithPaginationQueryValidator : AbstractValidator<GetLocationsWithPaginationQuery>
{
    public GetLocationsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber should be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should be greater than or equal to 1");

        RuleFor(x => x.SearchTerm)
            .NotEmpty().WithMessage("SearchTerm should not be empty");
    }
}
