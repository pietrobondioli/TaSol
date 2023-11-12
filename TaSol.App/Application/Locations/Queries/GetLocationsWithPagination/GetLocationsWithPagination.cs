namespace Application.Queries.Queries.GetLocationsWithPagination;

public record GetLocationsWithPaginationQuery : IRequest<GetLocationsWithPaginationDto>
{
    // Properties go here
}

public class
    GetLocationsWithPaginationQueryHandler : IRequestHandler<GetLocationsWithPaginationQuery,
        GetLocationsWithPaginationDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLocationsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetLocationsWithPaginationDto> Handle(GetLocationsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}