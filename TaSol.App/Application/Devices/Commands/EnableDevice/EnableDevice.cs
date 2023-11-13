using Domain.Entities;

namespace Application.Commands.Commands.EnableDevice;

public record EnableDeviceCommand : IRequest<long>
{
    public string? DeviceId { get; init; }
}

public class EnableDeviceCommandHandler : IRequestHandler<EnableDeviceCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public EnableDeviceCommandHandler(IUser user, IApplicationDbContext context)
    {
        _user = user;
        _context = context;
    }

    public async Task<long> Handle(EnableDeviceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Devices.FindAsync(request.DeviceId, cancellationToken);

        if (entity == null) throw new NotFoundException(nameof(Device), request.DeviceId);

        if (entity.OwnerId != _user.Id) throw new ForbiddenAccessException();

        entity.IsActive = true;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
