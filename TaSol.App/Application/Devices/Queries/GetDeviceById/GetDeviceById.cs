using Domain.Entities;

namespace Application.Queries.Queries.GetDeviceById;

public record GetDeviceByIdQuery : IRequest<GetDeviceByIdDto>
{
    public long DeviceId { get; init; }
}

public class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, GetDeviceByIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDeviceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetDeviceByIdDto> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await _context.Devices
            .AsNoTracking()
            .Include(d => d.Location)
            .FirstOrDefaultAsync(d => d.Id == request.DeviceId, cancellationToken);

        if (device == null) throw new NotFoundException(nameof(Device), request.DeviceId);

        return _mapper.Map<GetDeviceByIdDto>(device);
    }
}
