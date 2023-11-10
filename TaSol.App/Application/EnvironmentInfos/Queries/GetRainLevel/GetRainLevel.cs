using Application.Common.Interfaces;

namespace Application.Queries.Queries.GetRainLevel;

public record GetRainLevelQuery : IRequest<GetRainLevelDto>
{
    // Properties go here
}

public class GetRainLevelQueryHandler : IRequestHandler<GetRainLevelQuery, GetRainLevelDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRainLevelQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetRainLevelDto> Handle(GetRainLevelQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
