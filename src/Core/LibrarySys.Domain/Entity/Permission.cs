

namespace LibrarySys.Domain.Entity
{
    public class Permission : BaseClass
    {
        public Guid Id { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
        public string? Description { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}
