using Domain.Entities;

namespace Application.Devices.Commands.UpdateDevice;

public record UpdateDeviceCommand : IRequest<long>
{
    public string DeviceId { get; init; } = null!;

    public string? Name { get; init; }

    public string? Description { get; init; }
}

public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public UpdateDeviceCommandHandler(IUser user, IApplicationDbContext context)
    {
        _user = user;
        _context = context;
    }

    public async Task<long> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Devices.FindAsync(request.DeviceId, cancellationToken);

        if (entity == null) throw new NotFoundException(nameof(Device), request.DeviceId);

        if (entity.OwnerId != _user.Id) throw new ForbiddenAccessException();

        entity.Name = request.Name ?? entity.Name;
        entity.Description = request.Description ?? entity.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}