using FDEV.ISDDB.Domain.Identity.Auth;

namespace FDEV.ISDDB.Infrastructure.EFCore;

public static class DbContextExtension
{
    public static bool AllMigrationsApplied(this DbContext context)
    {
        var applied = context.GetService<IHistoryRepository>()
            .GetAppliedMigrations()
            .Select(m => m.MigrationId);

        var total = context.GetService<IMigrationsAssembly>()
            .Migrations
            .Select(m => m.Key);

        return !total.Except(applied).Any();
    }

    //public static void EnsureSeeded(this DbContext context)
    //{

    //    if (!context.Accounts.Any())
    //    {
    //        var accounts = DeserializeObject<List<Account>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "accounts.json"));
    //        context.AddRange(accounts);
    //        context.SaveChanges();
    //    }

    //    //Ensure we have some status
    //    if (!context.Users.Any())
    //    {
    //        var users = DeserializeObject<List<User>>(File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "users.json"));
    //        context.AddRange(users);
    //        context.SaveChanges();
    //    }
    //}
}