
namespace LibrarySys.Domain.Entity
{
    public class UserPermission : BaseClass
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
