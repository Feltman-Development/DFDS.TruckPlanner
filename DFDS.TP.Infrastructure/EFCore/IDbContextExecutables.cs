namespace FDEV.ISDDB.Infrastructure.EFCore;

/// <summary>
/// Interface with executable functions for the data context.
/// </summary>
public interface IDbContextExecutables
{
    int ExecuteSQL(string sqlString, IEnumerable<object> parameters);

    Task<int> ExecuteSQLAsync(string sqlString, params object[] parameters);


    int ExecuteSQL(string sqlString, params object[] parameters);

    Task<int> ExecuteSQLAsync(string sqlString, IEnumerable<object> parameters);


    int SaveChanges();

    int SaveChanges(bool acceptAllChangesOnSuccess);


    Task<int> SaveChangesAsync(CancellationToken cancelToken);

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancelToken);
}