using Domain.Entities;

namespace Application.Devices.Commands.ChangeDeviceLocation;

public record ChangeDeviceLocationCommand : IRequest<long>
{
    public long DeviceId { get; init; }

    public long NewLocationId { get; init; }
}

public class ChangeDeviceLocationCommandHandler : IRequestHandler<ChangeDeviceLocationCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public ChangeDeviceLocationCommandHandler(IApplicationDbContext context, IUserFactory userFactory)
    {
        _context = context;
        _user = userFactory.CreateUser();
    }

    public async Task<long> Handle(ChangeDeviceLocationCommand request, CancellationToken cancellationToken)
    {
        var device = await _context.Devices.FindAsync(request.DeviceId, cancellationToken);

        if (device == null)
        {
            throw new NotFoundException(nameof(Device), request.DeviceId);
        }

        if (device.OwnerId != _user.Id)
        {
            throw new ForbiddenAccessException();
        }

        var location = await _context.Locations.FindAsync(request.NewLocationId, cancellationToken);

        device.LocationId = location.Id;
        await _context.SaveChangesAsync(cancellationToken);

        return device.Id;
    }
}
