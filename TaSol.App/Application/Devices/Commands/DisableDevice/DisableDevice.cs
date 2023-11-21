using Domain.Entities;

namespace Application.Devices.Commands.DisableDevice;

public record DisableDeviceCommand : IRequest<long>
{
    public long DeviceId { get; init; }
}

public class DisableDeviceCommandHandler : IRequestHandler<DisableDeviceCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public DisableDeviceCommandHandler(IUser user, IApplicationDbContext context)
    {
        _user = user;
        _context = context;
    }

    public async Task<long> Handle(DisableDeviceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Devices.FindAsync(request.DeviceId, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Device), request.DeviceId);
        }

        if (entity.OwnerId != _user.Id)
        {
            throw new ForbiddenAccessException();
        }

        entity.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
