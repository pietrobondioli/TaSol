using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class EnvironmentInfo : BaseAuditableEntity
{
    public double Temperature { get; set; }

    public int Humidity { get; set; }

    public int LightLevel { get; set; }

    public int RainLevel { get; set; }

    public long DeviceId { get; set; }

    [ForeignKey(nameof(DeviceId))] public Device Device { get; set; } = null!;

    public long LocationId { get; set; }

    [ForeignKey(nameof(LocationId))] public Location Location { get; set; } = null!;
}
