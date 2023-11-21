using Domain.Entities;

namespace Application.EnvironmentInfos.Commands.CreateEnvironmentInfo;

public record CreateEnvironmentInfoCommand : IRequest<long>
{
    public double Temperature { get; init; }

    public int Humidity { get; init; }

    public int LightLevel { get; init; }

    public int RainLevel { get; init; }

    public string AuthToken { get; init; } = null!;
}

public class CreateEnvironmentInfoCommandHandler : IRequestHandler<CreateEnvironmentInfoCommand, long>
{
    private readonly IApplicationDbContext _context;

    private readonly ISecurityUtils _securityUtils;

    public CreateEnvironmentInfoCommandHandler(IApplicationDbContext context, ISecurityUtils securityUtils)
    {
        _context = context;
        _securityUtils = securityUtils;
    }

    public async Task<long> Handle(CreateEnvironmentInfoCommand request, CancellationToken cancellationToken)
    {
        var authTokenHash = _securityUtils.HashPassword(request.AuthToken);

        var device = await _context.Devices
            .Include(d => d.Owner)
            .FirstOrDefaultAsync(d => _securityUtils.VerifyPassword(request.AuthToken, d.AuthTokenHash),
                cancellationToken);

        if (device == null)
        {
            throw new NotFoundException(nameof(Device), request.AuthToken);
        }

        var entity = new EnvironmentInfo
        {
            DeviceId = device.Id,
            LocationId = device.LocationId,
            Temperature = request.Temperature,
            Humidity = request.Humidity,
            LightLevel = request.LightLevel,
            RainLevel = request.RainLevel
        };

        _context.EnvironmentInfos.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
