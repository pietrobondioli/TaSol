using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; }

    public long? CreatedBy { get; set; }

    [ForeignKey(nameof(CreatedBy))] public User? CreatedByUser { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public long? LastModifiedBy { get; set; }

    [ForeignKey(nameof(LastModifiedBy))] public User? LastModifiedByUser { get; set; }
}