using Application.Common.Interfaces;

namespace Application.Queries.Queries.GetTemperature;

public record GetTemperatureQuery : IRequest<GetTemperatureDto>
{
    // Properties go here
}

public class GetTemperatureQueryHandler : IRequestHandler<GetTemperatureQuery, GetTemperatureDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTemperatureQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetTemperatureDto> Handle(GetTemperatureQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
