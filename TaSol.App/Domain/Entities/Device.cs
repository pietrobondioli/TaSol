using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Device : BaseAuditableEntity
{
    [Required] public string Name { get; set; } = null!;

    [Required] public string Description { get; set; } = null!;

    [Required] public string AuthTokenHash { get; set; } = null!;

    [Required] public bool IsActive { get; set; }

    [Required] public long LocationId { get; set; }

    [ForeignKey(nameof(LocationId))] public virtual Location Location { get; set; } = null!;

    public virtual ICollection<EnvironmentInfo> EnvironmentInfos { get; set; } = null!;
}
