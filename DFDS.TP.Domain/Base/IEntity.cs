namespace DFDS.TP.Domain.Base;

/// <summary>
/// Interface with the properties needed to setup persistence.
/// Note that some values have default implementation on the interface.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Get the unique Id for the Entity.
    /// </summary>
    [Key]
    Guid Uid { get; }

    /// <summary>
    /// Version of the instance (ie. how many times changed in Db).
    /// </summary>
    int Version { get; }


    #region Default Implementations

    /// <summary>
    /// Get if the Entity is persisted to database
    /// </summary>
    public bool IsPersisted => !IsTransient(this);
    private static bool IsTransient(IEntity? obj) => obj != null && Equals(obj.Uid, default);

    /// <summary>
    /// Set to suppress PropertyChanged events.
    /// </summary>
    bool SuppressPropertyChanged => false;

    /// <summary>
    /// Set to suppress CollectionChanged events.
    /// </summary>
    bool SuppressCollectionChanged => false;

    #endregion Default Implementations
}