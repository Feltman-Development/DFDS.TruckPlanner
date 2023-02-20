using FDEV.ISDDB.Infrastructure.EFCore_consolidate;

namespace FDEV.ISDDB.Infrastructure.EFCore;

/// <summary>
/// A very thought through implementation of the ambitious IReadOnlyRepository, with everything as parameters so
/// that you can create any and all queries exceptionally easy, using a few methods only!
/// </summary>
/// <typeparam name="TContext">The type of the context.</typeparam>
/// <seealso cref="IReadOnlyRepository" />
public class ReadOnlyRepository<TContext> : IReadOnlyRepository where TContext : DbContext
{
    protected readonly TContext Context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyRepositoryEF{TContext}"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public ReadOnlyRepository(TContext context) => Context = context;

    /// <summary>
    /// Gets a queryable result, only to be used internal in the repository (if public, always return IEnumerable'')
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="filter">The filter.</param>
    /// <param name="orderBy">The order by.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <returns></returns>
    protected virtual IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null) where TEntity : class, IEntity
    {
        includeProperties ??= string.Empty;
        IQueryable<TEntity> query = Context.Set<TEntity>();
        if (filter != null) query = query.Where(filter);
        query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        if (orderBy != null) query = orderBy(query);
        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Take(take.Value);

        return query;
    }

    /// <inheritdoc />
    public IEnumerable<TEntity> FindBy<TEntity>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TEntity : class, IEntity
    {
        var query = Context.Set<TEntity>().Where(predicate);
        return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }

    /// <inheritdoc />
    public virtual IEnumerable<TEntity> GetAll<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null) where TEntity : class, IEntity
        => GetQueryable(null, orderBy, includeProperties, skip, take).ToList();

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null) where TEntity : class, IEntity
        => await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();

    /// <inheritdoc />
    public virtual IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null) where TEntity : class, IEntity
        => GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null) where TEntity : class, IEntity
        => await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();

    /// <inheritdoc />
    public virtual TEntity GetOne<TEntity>(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "") where TEntity : class, IEntity => GetQueryable(filter, null, includeProperties).SingleOrDefault();

    /// <inheritdoc />
    public virtual async Task<TEntity> GetOneAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null) where TEntity : class, IEntity
        => await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();

    /// <inheritdoc />
    public virtual TEntity GetFirst<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class, IEntity
        => GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();

    /// <inheritdoc />
    public virtual async Task<TEntity> GetFirstAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null) where TEntity : class, IEntity
        => await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();

    /// <inheritdoc />
    public virtual TEntity Get<TEntity>(object id) where TEntity : class, IEntity => Context.Set<TEntity>().Find(id);

    /// <inheritdoc />
    public virtual ValueTask<TEntity> GetAsync<TEntity>(object id) where TEntity : class, IEntity => Context.Set<TEntity>().FindAsync(id);

    /// <inheritdoc />
    public virtual int Count<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, IEntity => GetQueryable(filter).Count();

    /// <inheritdoc />
    public virtual Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, IEntity => GetQueryable(filter).CountAsync();

    /// <inheritdoc />
    public virtual bool Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, IEntity => GetQueryable(filter).Any();

    /// <inheritdoc />
    public virtual Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, IEntity => GetQueryable(filter).AnyAsync();
}