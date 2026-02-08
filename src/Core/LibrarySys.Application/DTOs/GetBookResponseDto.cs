namespace LibrarySys.Application.DTOs
{
    public class GetBookResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genere { get; set; }
        public int PublishYear { get; set; }
        public int AvailableCopies { get; set; }
        public string Authors { get; set; }
    }
}
