using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Device : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string AuthTokenHash { get; set; } = null!;

    public long LocationId { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey(nameof(LocationId))]
    public Location Location { get; set; } = null!;

    public ICollection<EnvironmentInfo> EnvironmentInfos { get; set; } = null!;
}
