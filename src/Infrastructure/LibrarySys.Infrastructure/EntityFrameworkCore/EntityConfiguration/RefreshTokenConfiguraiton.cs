using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.EntityConfiguration
{
    public class RefreshTokenConfiguraiton : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable(nameof(RefreshToken),"Identity");
            builder.HasKey(c=> c.Id);
            builder.Property(c => c.Token)
                .IsRequired();
            builder.Property(c=> c.UserId)
                .IsRequired();
            builder.HasOne(c=> c.User).WithOne(c=> c.RefreshToken).HasForeignKey<RefreshToken>(c=> c.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
