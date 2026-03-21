namespace LibrarySys.Application.DTOs
{
    public class SetBorrowRequestDto
    {
        public List<Guid> Id { get; set; }
        public string Email { get; set; }
    }
}
