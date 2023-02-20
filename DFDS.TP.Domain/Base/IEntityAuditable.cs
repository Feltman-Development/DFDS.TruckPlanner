namespace DFDS.TP.Domain.Base;

/// <summary>
/// Extends the IEntity interface with DateTimes needed for a full audit trail
/// </summary>
public interface IEntityAuditable
{
    /// <summary>
    /// DateTime of creation.
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// Get the entity that created this (person, company, service or other system...).
    /// </summary>
    string CreatedBy { get; }

    /// <summary>
    /// Get the date of last modification.
    /// </summary>
    DateTime LastModifiedAt { get; }

    /// <summary>
    /// Get the the last part to modify entity part (person, service or other system...).
    /// </summary>
    string LastModifiedBy { get; }

    /// <summary>
    /// DateTime that the entry is valid from. If not set explicit, it equals CreateTime.
    /// </summary>
    DateTime ValidFrom { get; }

    /// <summary>
    /// DateTime that the entry is valid to. If not set, the entry is valid until replaced.
    /// </summary>
    DateTime ValidTo { get; }

    /// <summary>
    /// Get the date the entry was replaced, by a new entity.
    /// </summary>
    DateTime? ReplacedOn { get; }

    /// <summary>
    /// Get the entity that replaced this one, if such exist.
    /// </summary>
    IEntity? ReplacedBy { get; }

    /// <summary>
    /// Get the date of the "soft" deletion of the entity.
    /// </summary>
    DateTime? DeletedAt { get; }

    /// <summary>
    /// Get the entity that set the deletion date.
    /// </summary>
    IEntity? DeletedBy { get; }
}
