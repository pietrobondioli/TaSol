namespace Application.Queries.Queries.GetDevicesWithPagination;

public record GetDevicesWithPaginationQuery : IRequest<GetDevicesWithPaginationDto>
{
    // Properties go here
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
        throw new NotImplementedException();
    }
}