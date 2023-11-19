namespace Application.Queries.Queries.GetLocationsWithPagination;

public record GetLocationsWithPaginationQuery : IRequest<GetLocationsWithPaginationDto>
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string SearchTerm { get; set; }
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
        var query = _context.Locations.AsQueryable();

        query = query.Where(x => EF.Functions.FreeText(x.Name, request.SearchTerm) ||
                                 EF.Functions.FreeText(x.Description, request.SearchTerm) ||
                                 EF.Functions.FreeText(x.Address, request.SearchTerm) ||
                                 EF.Functions.FreeText(x.City, request.SearchTerm) ||
                                 EF.Functions.FreeText(x.State, request.SearchTerm) ||
                                 EF.Functions.FreeText(x.Country, request.SearchTerm) ||
                                 EF.Functions.FreeText(x.Latitude, request.SearchTerm) ||
                                 EF.Functions.FreeText(x.Longitude, request.SearchTerm));

        var totalItems = await query.CountAsync(cancellationToken);

        var locations = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new GetLocationsWithPaginationDto(_mapper.Map<List<LocationDto>>(locations), totalItems,
            request.PageNumber, request.PageSize);
    }
}
