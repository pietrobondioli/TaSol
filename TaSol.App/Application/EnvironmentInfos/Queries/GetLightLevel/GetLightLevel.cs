using Application.Common.Interfaces;

namespace Application.Queries.Queries.GetLightLevel;

public record GetLightLevelQuery : IRequest<GetLightLevelDto>
{
    // Properties go here
}

public class GetLightLevelQueryHandler : IRequestHandler<GetLightLevelQuery, GetLightLevelDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLightLevelQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetLightLevelDto> Handle(GetLightLevelQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
