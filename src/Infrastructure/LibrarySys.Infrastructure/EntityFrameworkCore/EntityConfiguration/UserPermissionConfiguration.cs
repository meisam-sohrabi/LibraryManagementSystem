using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.EntityConfiguration
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermission");
            builder.HasKey(c => new { c.UserId, c.PermissionId });

            builder.HasOne(c => c.User)
                .WithMany(c => c.UserPermissions)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Permission)
                .WithMany(c => c.UserPermissions)
                .HasForeignKey(c => c.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
