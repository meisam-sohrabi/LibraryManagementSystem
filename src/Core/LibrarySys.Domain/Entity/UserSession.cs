
namespace LibrarySys.Domain.Entity
{
    public class UserSession : BaseClass
    {
        public Guid Id { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
