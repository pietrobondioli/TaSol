namespace Application.Devices.Queries.GetDevicesWithPagination;

public record GetDevicesWithPaginationQuery : IRequest<GetDevicesWithPaginationDto>
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public string? DeviceName { get; init; }
}

public class
    GetDevicesWithPaginationQueryHandler : IRequestHandler<GetDevicesWithPaginationQuery, GetDevicesWithPaginationDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDevicesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetDevicesWithPaginationDto> Handle(GetDevicesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Devices.AsQueryable();

        if (!string.IsNullOrEmpty(request.DeviceName))
        {
            query = query.Where(x => x.Name.Contains(request.DeviceName)).OrderBy(x => x.Name);
        }

        var devices = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Include(x => x.Location)
            .ToListAsync(cancellationToken);

        return new GetDevicesWithPaginationDto(_mapper.Map<List<DeviceDto>>(devices), devices.Count,
            request.PageNumber, request.PageSize);
    }
}
