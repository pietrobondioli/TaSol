using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public class BaseOwnedAuditableEntity : BaseAuditableEntity
{
    public long OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public virtual User Owner { get; set; }
}
