namespace LibrarySys.Domain.Entity
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpireAt { get; set; }

        public string UserId { get; set; }
        public User? User { get; set; }
    }
}
