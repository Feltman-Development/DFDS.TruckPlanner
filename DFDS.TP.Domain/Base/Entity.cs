namespace DFDS.TP.Domain.Base;

public abstract class Entity : IEntity, IEntityAuditable
{
    protected Entity()
    {
        Uid = Guid.NewGuid();
        Version = 1;
        CreatedAt = DateTime.Now;
        CreatedBy = Environment.UserName;
        LastModifiedAt = DateTime.Now;
        LastModifiedBy = Environment.UserName;
        ValidFrom = DateTime.Now;
        ValidTo = DateTime.MaxValue;
        ReplacedOn = null;
        ReplacedBy = null;
        DeletedAt = null;
        DeletedBy = null;
        Relations = new List<IRelationship>();
    }

    /// <inheritdoc />
    [Key, Required]
    public Guid Uid { get; }

    /// <inheritdoc />
    [Required]
    public int Version { get; set; }

    /// <inheritdoc />
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    [Required]
    public string CreatedBy { get; }

    /// <inheritdoc />
    [Required]
    public DateTime LastModifiedAt { get; set; }

    /// <inheritdoc />
    [Required]
    public string LastModifiedBy { get; }

    /// <inheritdoc />
    [Required]
    public DateTime ValidFrom { get; }

    /// <inheritdoc />
    [Required]
    public DateTime ValidTo { get; }

    /// <inheritdoc />
    [Required, AllowNull]
    public DateTime? ReplacedOn { get; }

    /// <inheritdoc />
    [Required, AllowNull]
    public IEntity? ReplacedBy { get; }

    /// <inheritdoc />
    [Required, AllowNull]
    public DateTime? DeletedAt { get; }

    /// <inheritdoc />
    [Required, AllowNull]
    public IEntity? DeletedBy { get; }

    /// <inheritdoc />
    [Required]
    public IList<IRelationship> Relations { get; }

    /// <inheritdoc />
    [NotMapped]
    public virtual bool SuppressCollectionChanged { get; } = false;

    /// <inheritdoc />
    [NotMapped]
    public virtual bool SuppressPropertyChanged { get; } = false;
}
