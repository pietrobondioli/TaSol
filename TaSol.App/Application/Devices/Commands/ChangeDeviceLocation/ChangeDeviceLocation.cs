using Application.Locations.Commands.CreateLocation;
using Domain.Entities;
using Microsoft.VisualBasic;

namespace Application.Commands.Commands.ChangeDeviceLocation;

public record ChangeDeviceLocationCommand : IRequest<long>
{
    public long DeviceId { get; init; }

    public long NewLocationId { get; init; }
}

public class ChangeDeviceLocationCommandHandler : IRequestHandler<ChangeDeviceLocationCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUser _user;

    public ChangeDeviceLocationCommandHandler(IUser user, IApplicationDbContext context, IMediator mediator)
    {
        _user = user;
        _context = context;
        _mediator = mediator;
    }

    public async Task<long> Handle(ChangeDeviceLocationCommand request, CancellationToken cancellationToken)
    {
        var device = await _context.Devices.FindAsync(request.DeviceId, cancellationToken);

        if (device == null) throw new NotFoundException(nameof(Device), request.DeviceId);

        if (device.OwnerId != _user.Id) throw new ForbiddenAccessException();

        var location = await _context.Locations.FindAsync(request.NewLocationId, cancellationToken);

        if (location == null) throw new NotFoundException(nameof(Location), request.NewLocationId);

        if (location.OwnerId != _user.Id) throw new ForbiddenAccessException();

        device.LocationId = location.Id;
        await _context.SaveChangesAsync(cancellationToken);

        return device.Id;
    }
}