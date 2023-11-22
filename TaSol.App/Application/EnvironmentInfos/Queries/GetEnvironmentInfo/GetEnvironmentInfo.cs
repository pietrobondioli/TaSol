using Application.EnvironmentInfos.Constants;

namespace Application.EnvironmentInfos.Queries.GetEnvironmentInfo;

public record GetEnvironmentInfoQuery : IRequest<GetEnvironmentInfoDto>
{
    public long LocationId { get; init; }

    public long? DeviceId { get; init; }

    public QueryRange.Options Range { get; init; }

    public QueryInterval.Options Interval { get; init; }
}

public class GetEnvironmentInfoQueryHandler : IRequestHandler<GetEnvironmentInfoQuery, GetEnvironmentInfoDto>
{
    private readonly IApplicationDbContext _context;

    public GetEnvironmentInfoQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetEnvironmentInfoDto> Handle(GetEnvironmentInfoQuery request,
        CancellationToken cancellationToken)
    {
        var endDate = DateTime.UtcNow;
        var startDate = QueryRange.GetRelativeDate(request.Range, endDate);

        var query = _context.EnvironmentInfos
            .Where(e => e.LocationId == request.LocationId &&
                        (!request.DeviceId.HasValue || e.DeviceId == request.DeviceId.Value) &&
                        e.TimeStamp >= startDate && e.TimeStamp <= endDate);
        
        var interval = QueryInterval.GetIntervalDuration(request.Interval);
        var groupedData = await query
            .GroupBy(e => new { Interval = EF.Functions.DateDiffMinute(startDate, e.TimeStamp) / interval })
            .Select(g => new HumidityDataPoint
            {
                StartTime = startDate.AddMinutes(g.Key.Interval * interval),
                AverageHumidity = g.Average(e => e.Humidity),
                AverageTemperature = g.Average(e => e.Temperature),
                AverageLightLevel = g.Average(e => e.LightLevel),
                AverageRainLevel = g.Average(e => e.RainLevel)
            })
            .ToListAsync(cancellationToken);

        return new GetEnvironmentInfoDto { DataPoints = groupedData };
    }
}
