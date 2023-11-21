using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class EnvironmentInfo : BaseEntity
{
    [Required] public double Temperature { get; set; }

    [Required] public int Humidity { get; set; }

    [Required] public int LightLevel { get; set; }

    [Required] public int RainLevel { get; set; }

    [Required] public DateTimeOffset TimeStamp { get; set; }

    [Required] public long DeviceId { get; set; }

    [ForeignKey(nameof(DeviceId))] public virtual Device Device { get; set; } = null!;

    [Required] public long LocationId { get; set; }

    [ForeignKey(nameof(LocationId))] public virtual Location Location { get; set; } = null!;
}
