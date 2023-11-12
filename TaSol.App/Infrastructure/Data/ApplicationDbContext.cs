using System.Linq.Expressions;
using System.Reflection;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<UserEmailResetToken> UserEmailResetTokens => Set<UserEmailResetToken>();

    public DbSet<UserEmailVerificationToken> UserEmailVerificationTokens => Set<UserEmailVerificationToken>();

    public DbSet<UserPasswordResetToken> UserPasswordResetTokens => Set<UserPasswordResetToken>();

    public DbSet<UserMetadata> UserMetadata => Set<UserMetadata>();

    public DbSet<Device> Devices => Set<Device>();

    public DbSet<Location> Locations => Set<Location>();

    public DbSet<EnvironmentInfo> EnvironmentInfos => Set<EnvironmentInfo>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        foreach (var ClrType in builder.Model.GetEntityTypes()
                     .Where(e => typeof(BaseAuditableEntity).IsAssignableFrom(e.ClrType)).Select(e => e.ClrType))
        {
            var parameter = Expression.Parameter(ClrType, "e");
            var body = Expression.MakeMemberAccess(parameter, ClrType.GetProperty("IsDeleted"));
            var checkDeleted = Expression.MakeUnary(ExpressionType.Not, body, typeof(bool));
            var lambda = Expression.Lambda(checkDeleted, parameter);

            builder.Entity(ClrType).HasQueryFilter(lambda);
        }
    }
}