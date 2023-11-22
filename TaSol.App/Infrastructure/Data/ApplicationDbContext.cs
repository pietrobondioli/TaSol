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

        ConfigureRelationships(builder);

        base.OnModelCreating(builder);
    }

    private void ConfigureRelationships(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(x => x.PasswordResetTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<User>()
            .HasMany(x => x.EmailResetTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<User>()
            .HasMany(x => x.EmailVerificationTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<User>()
            .HasMany(x => x.Devices)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Device>()
            .HasMany(x => x.EnvironmentInfos)
            .WithOne(x => x.Device)
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Device>()
            .HasOne(x => x.Location)
            .WithMany(x => x.Devices)
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Location>()
            .HasMany(x => x.Devices)
            .WithOne(x => x.Location)
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Location>()
            .HasMany(x => x.EnvironmentInfos)
            .WithOne(x => x.Location)
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.NoAction);

        foreach (var auditableEntity in builder.Model.GetEntityTypes()
                     .Where(e => typeof(BaseAuditableEntity).IsAssignableFrom(e.ClrType))
                     .Select(e => e.ClrType))
        {
            builder.Entity(auditableEntity)
                .HasOne(typeof(User), nameof(BaseAuditableEntity.CreatedByUser))
                .WithMany()
                .HasForeignKey(nameof(BaseAuditableEntity.CreatedBy))
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity(auditableEntity)
                .HasOne(typeof(User), nameof(BaseAuditableEntity.LastModifiedByUser))
                .WithMany()
                .HasForeignKey(nameof(BaseAuditableEntity.LastModifiedBy))
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity(auditableEntity)
                .HasOne(typeof(User), nameof(BaseAuditableEntity.DeletedByUser))
                .WithMany()
                .HasForeignKey(nameof(BaseAuditableEntity.DeletedBy))
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
