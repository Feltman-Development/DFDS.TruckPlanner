using FDEV.ISDDB.Core.Data;
using FDEV.ISDDB.Domain.Entities.Identity.Auth;
using FDEV.ISDDB.Domain.Entities.Relations;

namespace FDEV.ISDDB.Infrastructure.EFCore;

public class AppDbContext : DbContext, DbContextBaseImpl, IDbContext
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(TempDevData.Database.ConnectionStringProd);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {

    }

    #region Repositories

    public DbSet<Developer> Rules { get; private set; }

    public DbSet<Company> Orders { get; private set; }

    public DbSet<Software> Customers { get; private set; }

    public DbSet<Relationship> Employees { get; private set; }

    public DbSet<Role> Promotions { get; private set; }

    #endregion Repositories
}
