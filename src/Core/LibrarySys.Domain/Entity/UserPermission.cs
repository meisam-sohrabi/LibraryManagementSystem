namespace LibrarySys.Domain.Entity
{
    public class UserPermission
    {
        public string UserId { get; set; }
        public CustomUser User { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
