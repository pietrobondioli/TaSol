using Domain.Entities;
using Domain.Events;

namespace Application.Devices.Commands.CreateDevice;

public record CreateDeviceCommand : IRequest<(long, string)>
{
    public string Name { get; init; }

    public string Description { get; init; }

    public long LocationId { get; init; }
}

public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, (long, string)>
{
    private readonly IApplicationDbContext _context;

    private readonly ISecurityUtils _securityUtils;

    private readonly IUser _user;

    public CreateDeviceCommandHandler(IUser user, IApplicationDbContext context, ISecurityUtils securityUtils)
    {
        _context = context;
        _user = user;
        _securityUtils = securityUtils;
    }

    public async Task<(long, string)> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        var token = _securityUtils.GenerateRandomApiKey();
        
        var entity = new Device
        {
            Name = request.Name,
            Description = request.Description,
            LocationId = request.LocationId,
            OwnerId = _user.Id!.Value,
            AuthTokenHash = _securityUtils.HashPassword(token)
        };

        entity.AddDomainEvent(new DeviceCreatedEvent(entity));

        _context.Devices.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return (entity.Id, token);
    }
}
