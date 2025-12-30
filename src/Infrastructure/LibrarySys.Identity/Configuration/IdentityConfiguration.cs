using LibrarySys.Identity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Identity.Configuration
{
    internal class IdentityConfiguration : IEntityTypeConfiguration<CustomUser>
    {
        public void Configure(EntityTypeBuilder<CustomUser> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("User", "user");
            builder.Property(c => c.FullName).HasMaxLength(60).IsRequired();
        }
    }
}
