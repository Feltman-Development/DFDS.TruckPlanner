using FDEV.ISDDB.Infrastructure.EFCore_consolidate;

namespace FDEV.ISDDB.Infrastructure.EFCore;

/// <summary>
/// Generic CRUD interface extending the read only interface
/// </summary>
/// <seealso cref="IReadOnlyRepository" />
public interface IReadAndWriteRepository : IReadOnlyRepository
{
    /// <summary>
    /// Creates the specified entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity.</param>
    /// <param name="createdBy">The created by.</param>
    void Create<TEntity>(TEntity entity, IEntity createdBy = null) where TEntity : class, IEntity;

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity.</param>
    /// <param name="modifiedBy">The modified by.</param>
    void Update<TEntity>(TEntity entity, IEntity modifiedBy = null) where TEntity : class, IEntity;

    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The identifier.</param>
    void Delete<TEntity>(object id) where TEntity : class, IEntity;

    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity.</param>
    void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
}