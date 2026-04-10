namespace LibrarySys.Application.DTOs
{
    public class CurrentUserResponseDto
    {
        public string? UserId { get; set; }
        public string IPAddress { get; set; }
        public string Email { get; set; }
    }
}