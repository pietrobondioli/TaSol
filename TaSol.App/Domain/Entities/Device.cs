using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Device : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Location Location { get; set; } = null!;

    public int OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))] public virtual User Owner { get; set; } = null!;
    
    public ICollection<EnvironmentInfo> EnvironmentInfos { get; set; } = null!;
}