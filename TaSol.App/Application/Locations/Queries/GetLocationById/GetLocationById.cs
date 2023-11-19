using Domain.Entities;

namespace Application.Queries.Queries.GetLocationById;

public record GetLocationByIdQuery : IRequest<GetLocationByIdDto>
{
    public int LocationId { get; set; }
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
        var location = await _context.Locations
            .FirstOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken);

        if (location == null) throw new NotFoundException(nameof(Location), request.LocationId);

        return _mapper.Map<GetLocationByIdDto>(location);
    }
}
