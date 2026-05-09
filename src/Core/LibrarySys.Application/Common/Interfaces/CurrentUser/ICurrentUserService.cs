namespace LibrarySys.Application.Common.Interfaces.CurrentUser
{
    public interface ICurrentUserService
    {
        public string? UserId { get; }
        public string IPAddress { get; }
        public string Email { get; }
    }
}
