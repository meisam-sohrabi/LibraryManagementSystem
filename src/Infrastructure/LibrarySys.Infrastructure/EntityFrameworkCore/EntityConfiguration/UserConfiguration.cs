using LibrarySys.Application.Common.Interfaces.Infrastructure.UserContract;
using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibrarySys.Domain.Enum;
namespace LibrarySys.Infrastructure.EntityFrameworkCore.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Identity");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.HasIndex(c => c.Email)
                .HasDatabaseName("IX_User_Email");
            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.UserName)
               .IsRequired()
               .HasMaxLength(256);
            builder.Property(c => c.Email)
               .IsRequired(false)
               .HasMaxLength(256);
            builder.Property(c => c.PasswordHash)
               .IsRequired(false);
            builder.Property(c => c.PhoneNumber)
               .IsRequired(false)
               .HasMaxLength(13);
            builder.Property(c => c.Role).HasConversion<string>();
            builder.Property(c => c.UserStatus).HasConversion<string>();

        }


    }
}
