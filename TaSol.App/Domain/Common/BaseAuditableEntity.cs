using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public long OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))] public virtual User Owner { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public long CreatedBy { get; set; }

    [ForeignKey(nameof(CreatedBy))] public virtual User CreatedByUser { get; set; }

    public DateTimeOffset LastModifiedAt { get; set; }

    public long LastModifiedBy { get; set; }

    [ForeignKey(nameof(LastModifiedBy))] public virtual User LastModifiedByUser { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public long? DeletedBy { get; set; }

    [ForeignKey(nameof(DeletedBy))] public virtual User? DeletedByUser { get; set; }

    public void SetCreated(long userId)
    {
        CreatedAt = DateTimeOffset.UtcNow;
        CreatedBy = userId;
    }

    public void SetLastModified(long userId)
    {
        LastModifiedAt = DateTimeOffset.UtcNow;
        LastModifiedBy = userId;
    }

    public void SetDeleted(long userId)
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
        DeletedBy = userId;
    }
}
