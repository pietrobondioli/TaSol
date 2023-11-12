using Domain.Entities;

namespace Application.Devices.Commands.DeleteDevice;

public record DeleteDeviceCommand : IRequest<long>
{
    public string DeviceId { get; init; } = null!;
}

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public DeleteDeviceCommandHandler(IUser user, IApplicationDbContext context)
    {
        _user = user;
        _context = context;
    }

    public async Task<long> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Devices.FindAsync(request.DeviceId, cancellationToken);

        if (entity == null) throw new NotFoundException(nameof(Device), request.DeviceId);

        if (entity.OwnerId != _user.Id) throw new ForbiddenAccessException();

        _context.Devices.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}