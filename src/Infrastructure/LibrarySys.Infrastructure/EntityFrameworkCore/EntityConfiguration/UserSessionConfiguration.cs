using LibrarySys.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.EntityConfiguration
{
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("UserSession", "Identity");
            builder.HasKey(x => x.Id);

            //  Single Indexes (کوئری‌های ساده)
            builder.HasIndex(c => c.UserName)
                .HasDatabaseName("IX_UserSession_UserName");
            builder.HasIndex(c => c.LoginTime)
                .HasDatabaseName("IX_UserSession_LoginTime");
            builder.HasIndex(c => c.LogoutTime)
                .HasDatabaseName("IX_UserSession_LogoutTime");

            //  Composite Indexes (کوئری‌های واقعی!)
            builder.HasIndex(c => new { c.UserName, c.LogoutTime })
                .HasDatabaseName("IX_UserSession_UserName_LogoutTime");     
            builder.HasIndex(c => new { c.UserName, c.LoginTime })
                .HasDatabaseName("IX_UserSession_UserName_LoginTime");
        }
    }
}
