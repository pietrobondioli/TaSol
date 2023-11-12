namespace Application.Queries.Queries.GetHumidityLevel;

public record GetHumidityLevelQuery : IRequest<GetHumidityLevelDto>
{
    // Properties go here
}

public class GetHumidityLevelQueryHandler : IRequestHandler<GetHumidityLevelQuery, GetHumidityLevelDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetHumidityLevelQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetHumidityLevelDto> Handle(GetHumidityLevelQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}