namespace LibrarySys.Domain.Entity
{
    public class CustomUser
    {
        public string Id { get; set; }
        public string FullName { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}