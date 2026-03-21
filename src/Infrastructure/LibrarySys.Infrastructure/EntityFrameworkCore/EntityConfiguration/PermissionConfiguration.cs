using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.EntityConfiguration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(nameof(Permission));
            builder.HasKey(c=> c.Id);
            builder.Property(p => p.Resource).IsRequired().HasMaxLength(120);
            builder.Property(p => p.Action).IsRequired().HasMaxLength(120);
        }
    }
}
