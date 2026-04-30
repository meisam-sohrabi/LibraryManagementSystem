using LibrarySys.Domain.Enum;

namespace LibrarySys.Domain.Entity
{
    public class User : BaseClass
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public UserActivity UserStatus { get; set; }

        public RefreshToken? RefreshToken { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

    }
}
