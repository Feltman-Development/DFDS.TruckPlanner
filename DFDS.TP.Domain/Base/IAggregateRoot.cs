namespace DFDS.TP.Domain.Base;

/// <summary>
/// Apply this marker interface to identify entities that serve as an aggregating unit when persisting data.
/// The concept of an aggregate root is part of the Domain Driven Development (DDD) architectural strategy.
/// </summary>
/// <remarks>
/// - A root is the boundary for an aggregate (hence the name) of a collection of entities, that exits only as part of the aggregate and have no global identity.
/// - Root Entities have global identity. Entities inside the boundary have local identity, unique only within the Aggregate.
/// - Nothing outside the Aggregate boundary can hold a reference to anything inside, except to the root Entity.
/// - Only Aggregate Roots can be obtained directly with database queries.
/// - Objects within the Aggregate can hold references to other Aggregate roots.
/// </remarks>
public interface IAggregateRoot
{
    /// <summary>
    /// Gets whether the current aggregate can be saved.
    /// </summary>
    bool CanBeSaved { get; }

    /// <summary>
    /// Gets whether the current aggregate can be updated.
    /// </summary>
    bool CanBeUpdated { get; }

    /// <summary>
    /// Gets whether the current aggregate can be deleted.
    /// If an aggregate cannot be deleted, it will change state to Modified and a DeletedAt date wil be set instead, when saving changes on context
    /// </summary>
    bool CanBeDeleted { get; }

    /// <summary>
    /// Gets a date from where this aggregate no longer is active (soft deleted) and will be excluded from queries.
    /// NOTE: Rather than using this use the calculated property <seealso cref="IsDeleted"/>.
    /// </summary>
    /// <remarks> Rather than using this use the calculated property <seealso cref="IsDeleted"/>. </remarks>
    DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Get the entity that set the deletion date.
    /// </summary>
    IEntity? DeletedBy { get; }

    /// <summary>
    /// Get if the entity is soft deleted.
    /// Default implementation on interface, so that it's not needed down the line and wom't be included in data model.
    /// </summary>
    public bool IsDeleted => DeletedAt != default && DeletedAt < DateTime.UtcNow;
}
