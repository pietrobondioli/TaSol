using Application.Common.Interfaces;

namespace Application.Queries.Queries.GetLocationById;

public record GetLocationByIdQuery : IRequest<GetLocationByIdDto>
{
    // Properties go here
}

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, GetLocationByIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLocationByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetLocationByIdDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
