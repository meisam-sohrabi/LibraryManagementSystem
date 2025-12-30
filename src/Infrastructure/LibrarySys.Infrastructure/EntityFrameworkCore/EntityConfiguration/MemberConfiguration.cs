using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.EntityConfiguration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(c=> c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.ToTable("Members", "member");
            builder.Property(c => c.Email).HasMaxLength(200).IsRequired(required:true);
            builder.HasMany(c => c.Borrowings)
                .WithOne(c => c.Member)
                .HasForeignKey(c => c.MemberId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
        }
    }
}
