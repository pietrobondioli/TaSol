using Domain.Entities;

namespace Application.Commands.Commands.ReactivateDevice;

public record ReactivateDeviceCommand : IRequest<long>
{
    public string? DeviceId { get; init; }
}

public class ReactivateDeviceCommandHandler : IRequestHandler<ReactivateDeviceCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public ReactivateDeviceCommandHandler(IUser user, IApplicationDbContext context)
    {
        _user = user;
        _context = context;
    }

    public async Task<long> Handle(ReactivateDeviceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Devices.FindAsync(request.DeviceId, cancellationToken);

        if (entity == null) throw new NotFoundException(nameof(Device), request.DeviceId);

        if (entity.OwnerId != _user.Id) throw new ForbiddenAccessException();

        entity.IsDeleted = false;
        entity.DeletedAt = null;
        entity.DeletedBy = null;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}