using Domain.Entities;

namespace Application.EnvironmentInfos.Commands.CreateEnvironmentInfo;

public record CreateEnvironmentInfoCommand : IRequest<long>
{
    public double Temperature { get; init; }

    public double Humidity { get; init; }

    public double LightLevel { get; init; }

    public double RainLevel { get; init; }

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

        var devices = await _context.Devices
            .Include(d => d.Owner)
            .Where(d => !d.IsDeleted)
            .ToListAsync(cancellationToken);

        var device = devices.FirstOrDefault(d => _securityUtils.VerifyPassword(request.AuthToken, d.AuthTokenHash));

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
