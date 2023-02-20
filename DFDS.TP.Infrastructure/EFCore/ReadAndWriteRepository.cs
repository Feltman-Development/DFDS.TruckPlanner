namespace FDEV.ISDDB.Infrastructure.EFCore;

/// <summary>
/// EF Core implementation of the generic repository, consolidating the read only repository and the CRUD repository
/// </summary>
/// <typeparam name="TContext">The type of the context.</typeparam>
/// <seealso cref="ReadOnlyRepositoryEF{TContext}" />
/// <seealso cref="IWriteRepository" />
public class ReadAndWriteRepository<TContext> : ReadOnlyRepository<TContext>, IReadAndWriteRepository where TContext : DbContext
{
    public ReadAndWriteRepository(TContext context) : base(context)
    {
    }

    public virtual void Create<TEntity>(TEntity entity, IEntity createdBy = null) where TEntity : class, IEntity
    {
        entity.CreatedAt = DateTime.UtcNow;
        Context.Set<TEntity>().Add(entity);
    }

    public virtual void Update<TEntity>(TEntity entity, IEntity modifiedBy = null) where TEntity : class, IEntity
    {
        entity.LastUpdatedAt = DateTime.UtcNow;
        Context.Set<TEntity>().Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete<TEntity>(object id) where TEntity : class, IEntity
    {
        var entity = Context.Set<TEntity>().Find(id);
        Delete(entity);
    }

    public virtual void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        var dbSet = Context.Set<TEntity>();
        if (Context.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }
        dbSet.Remove(entity);
    }

    public virtual void SaveChanges()
    {
        try
        {
            Context.SaveChanges();
        }
        catch (DbEntityValidationException e)
        {
            ThrowEnhancedValidationException(e);
        }
    }

    public virtual Task SaveChangesAsync()
    {
        try
        {
            return Context.SaveChangesAsync();
        }
        catch (DbEntityValidationException e)
        {
            ThrowEnhancedValidationException(e);
        }
        return Task.FromResult(0);
    }

    protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
    {
        var errorMessages = e.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
        var fullErrorMessage = string.Join("; ", errorMessages);
        var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
        throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
    }
}