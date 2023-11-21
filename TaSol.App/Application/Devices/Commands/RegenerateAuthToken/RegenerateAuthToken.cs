using Domain.Entities;

namespace Application.Devices.Commands.RegenerateAuthToken;

public record RegenerateAuthTokenCommand : IRequest<long>
{
    public long DeviceId { get; init; }
}

public class RegenerateAuthTokenCommandHandler : IRequestHandler<RegenerateAuthTokenCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly ISecurityUtils _securityUtils;
    private readonly IUser _user;

    public RegenerateAuthTokenCommandHandler(IUser user, IApplicationDbContext context, ISecurityUtils securityUtils)
    {
        _user = user;
        _context = context;
        _securityUtils = securityUtils;
    }

    public async Task<long> Handle(RegenerateAuthTokenCommand request, CancellationToken cancellationToken)
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

        entity.AuthTokenHash = _securityUtils.HashPassword(_securityUtils.GenerateRandomApiKey());

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
