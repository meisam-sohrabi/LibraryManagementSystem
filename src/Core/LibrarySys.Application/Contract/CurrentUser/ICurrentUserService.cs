namespace LibrarySys.Application.Contract.CurrentUser
{
    public interface ICurrentUserService
    {
        public string? UserId { get; }
        public string IPAddress { get; }
        public string Email { get; }
    }
}
