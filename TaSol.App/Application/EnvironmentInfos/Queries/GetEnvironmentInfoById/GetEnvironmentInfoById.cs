namespace Application.Queries.Queries.GetEnvironmentInfoById;

public record GetEnvironmentInfoByIdQuery : IRequest<GetEnvironmentInfoByIdDto>
{
    // Properties go here
}

public class
    GetEnvironmentInfoByIdQueryHandler : IRequestHandler<GetEnvironmentInfoByIdQuery, GetEnvironmentInfoByIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEnvironmentInfoByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetEnvironmentInfoByIdDto> Handle(GetEnvironmentInfoByIdQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}