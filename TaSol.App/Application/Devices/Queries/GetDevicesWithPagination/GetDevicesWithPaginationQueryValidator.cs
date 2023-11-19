namespace Application.Queries.Queries.GetDevicesWithPagination;

public class GetDevicesWithPaginationQueryValidator : AbstractValidator<GetDevicesWithPaginationQuery>
{
    public GetDevicesWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber should be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should be greater than or equal to 1");

        RuleFor(x => x.DeviceName)
            .MaximumLength(50).WithMessage("DeviceName should not be more than 50 characters");
    }
}
