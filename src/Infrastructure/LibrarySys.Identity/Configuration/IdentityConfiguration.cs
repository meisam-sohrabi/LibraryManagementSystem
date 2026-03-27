using LibrarySys.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Identity.Configuration
{
    public class IdentityConfiguration : IEntityTypeConfiguration<CustomUser>
    {
        public void Configure(EntityTypeBuilder<CustomUser> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FullName).HasMaxLength(60).IsRequired();
            builder.HasData(AdminData());
        }


        private CustomUser AdminData()
        {
            PasswordHasher<CustomUser> passwordHasher = new PasswordHasher<CustomUser>();
            CustomUser admin = new CustomUser
            {
                Id = "4e98c3e0-5319-438b-8a6e-fa370d8635af",
                UserName = "admin@example",
                NormalizedUserName = "admin@example".ToUpper(),
                PhoneNumber = "",
                Email = "admin@example.com",
                NormalizedEmail = "admin@example.com".ToUpper(),
                FullName = "admin",
                PasswordHash = passwordHasher.HashPassword(null,"Admin@123"),
                SecurityStamp = Guid.NewGuid().ToString()

            };
            return admin;
        }
    }
}
