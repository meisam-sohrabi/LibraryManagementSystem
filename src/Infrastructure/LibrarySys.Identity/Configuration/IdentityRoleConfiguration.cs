using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Identity.Configuration
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(AdminRoleData());
        }

        private IdentityRole AdminRoleData()
        {
            IdentityRole role = new IdentityRole
            {
                Id = "c5e81656-de68-4fdc-90a1-be78c5dd3d67",
                Name = "admin",
                NormalizedName = "admin".ToUpper(),
            };

            return role;
        }
    }
}
