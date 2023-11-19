using Domain.Entities;

namespace Application.Queries.Queries.GetEnvironmentInfo;

public class GetEnvironmentInfoDto
{
    public List<HumidityDataPoint> DataPoints { get; set; } = new List<HumidityDataPoint>();
}

public class HumidityDataPoint
{
    public DateTime StartTime { get; set; }
    public double AverageHumidity { get; set; }
    public double AverageTemperature { get; set; }
    public double AverageLightLevel { get; set; }
    public double AverageRainLevel { get; set; }
}
