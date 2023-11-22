using Domain.Entities;

namespace Application.Devices.Commands.RegenerateAuthToken;

public record RegenerateAuthTokenCommand : IRequest<(long, string)>
{
    public long DeviceId { get; init; }
}

public class RegenerateAuthTokenCommandHandler : IRequestHandler<RegenerateAuthTokenCommand, (long, string)>
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

    public async Task<(long, string)> Handle(RegenerateAuthTokenCommand request, CancellationToken cancellationToken)
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
        
        var token = _securityUtils.GenerateRandomApiKey();

        entity.AuthTokenHash = _securityUtils.HashPassword(token);

        await _context.SaveChangesAsync(cancellationToken);

        return (entity.Id, token);
    }
}
