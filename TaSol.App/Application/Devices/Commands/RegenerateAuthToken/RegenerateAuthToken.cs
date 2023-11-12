using Domain.Entities;

namespace Application.Commands.Commands.RegenerateAuthToken;

public record RegenerateAuthTokenCommand : IRequest<long>
{
    public string DeviceId { get; init; } = null!;
}

public class RegenerateAuthTokenCommandHandler : IRequestHandler<RegenerateAuthTokenCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public RegenerateAuthTokenCommandHandler(IUser user, IApplicationDbContext context)
    {
        _user = user;
        _context = context;
    }

    public async Task<long> Handle(RegenerateAuthTokenCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Devices.FindAsync(request.DeviceId, cancellationToken);

        if (entity == null) throw new NotFoundException(nameof(Device), request.DeviceId);

        if (entity.OwnerId != _user.Id) throw new ForbiddenAccessException();

        entity.AuthToken = Guid.NewGuid().ToString();

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}