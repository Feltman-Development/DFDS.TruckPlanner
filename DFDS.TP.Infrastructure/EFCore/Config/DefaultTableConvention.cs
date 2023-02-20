﻿using System.Data.Entity.ModelConfiguration;
using EFCore.NamingConventions.Internal;
using System.Globalization;

namespace FDEV.ISDDB.Infrastructure.EFCore.Config;

public class DefaultTableConvention : IConfigurationConvention<Type, EntityTypeConfiguration<>>
{
    public void Apply(Type type, Func<EntityTypeConfiguration> configuration)
    {
        configuration().ToTable("PE_" + type.Name);
    }
}

public static class NamingConventionsExtensions
{
    public static DbContextOptionsBuilder UseSnakeCaseNamingConvention([NotNull] this DbContextOptionsBuilder optionsBuilder, CultureInfo? culture = null)
    {
        //Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        var extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>() ?? new NamingConventionsOptionsExtension()).WithSnakeCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseSnakeCaseNamingConvention<TContext>([NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder, CultureInfo? culture = null) where TContext : DbContext
        => (DbContextOptionsBuilder<TContext>)UseSnakeCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);

    public static DbContextOptionsBuilder UseLowerCaseNamingConvention([NotNull] this DbContextOptionsBuilder optionsBuilder, CultureInfo? culture = null)
    {
        //Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        var extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>() ?? new NamingConventionsOptionsExtension()).WithLowerCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseLowerCaseNamingConvention<TContext>([NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder, CultureInfo? culture = null) where TContext : DbContext
        => (DbContextOptionsBuilder<TContext>)UseLowerCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);

    public static DbContextOptionsBuilder UseUpperCaseNamingConvention([NotNull] this DbContextOptionsBuilder optionsBuilder, CultureInfo? culture = null)
    {
        //Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        var extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>() ?? new NamingConventionsOptionsExtension()).WithUpperCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseUpperCaseNamingConvention<TContext>([NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder, CultureInfo? culture = null) where TContext : DbContext
        => (DbContextOptionsBuilder<TContext>)UseUpperCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);

    public static DbContextOptionsBuilder UseUpperSnakeCaseNamingConvention([NotNull] this DbContextOptionsBuilder optionsBuilder, CultureInfo? culture = null)
    {
        //Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        var extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>() ?? new NamingConventionsOptionsExtension()).WithUpperSnakeCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);
        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseUpperSnakeCaseNamingConvention<TContext>([NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder, CultureInfo? culture = null) where TContext : DbContext
        => (DbContextOptionsBuilder<TContext>)UseUpperSnakeCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);

    public static DbContextOptionsBuilder UseCamelCaseNamingConvention([NotNull] this DbContextOptionsBuilder optionsBuilder, CultureInfo culture = null)
    {
        //Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        var extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>() ?? new NamingConventionsOptionsExtension()).WithCamelCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);
        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseCamelCaseNamingConvention<TContext>([NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder, CultureInfo? culture = null) where TContext : DbContext
        => (DbContextOptionsBuilder<TContext>)UseCamelCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);
}
