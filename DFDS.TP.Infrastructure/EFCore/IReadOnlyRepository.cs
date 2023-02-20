namespace FDEV.ISDDB.Infrastructure.EFCore;

public interface IReadOnlyRepository<ConfigEntity>
{
    IEnumerable<TEntity> GetAll<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,string includeProperties = null);

    Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null);

    IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null);

    Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null);

    TEntity GetOne<TEntity>(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null);

    Task<TEntity> GetOneAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null);

    TEntity GetFirst<TEntity>(Expression<Func<TEntity, bool>> filter = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null);

    Task<TEntity> GetFirstAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,string includeProperties = null);

    TEntity GetById<TEntity>(object id);

    Task<TEntity> GetByIdAsync<TEntity>(object id);

    int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null);

    Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null);

    bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null);

    Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null);
}