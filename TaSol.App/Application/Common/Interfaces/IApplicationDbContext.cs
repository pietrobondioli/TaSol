using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    DbSet<UserEmailResetToken> UserEmailResetTokens { get; }

    DbSet<UserEmailVerificationToken> UserEmailVerificationTokens { get; }

    DbSet<UserPasswordResetToken> UserPasswordResetTokens { get; }

    DbSet<UserMetadata> UserMetadata { get; }

    DbSet<Device> Devices { get; }

    DbSet<Location> Locations { get; }

    DbSet<EnvironmentInfo> EnvironmentInfos { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
