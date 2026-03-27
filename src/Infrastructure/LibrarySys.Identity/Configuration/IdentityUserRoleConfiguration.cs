using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Identity.Configuration
{
    public class IdentityUserRoleConfiguration
    {
        public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
            {
                builder.HasData(AdminSetRole());
            }

            private IdentityUserRole<string> AdminSetRole()
            {
                IdentityUserRole<string> setRole = new IdentityUserRole<string>
                {
                    RoleId = "c5e81656-de68-4fdc-90a1-be78c5dd3d67",
                    UserId = "4e98c3e0-5319-438b-8a6e-fa370d8635af"
                };
                return setRole;
            }
        }
    }
}