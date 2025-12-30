namespace LibrarySys.Application.DTOs
{
    public class BookAuthorRequestDto 
    {
        public string Title { get; set; }
        public string Genere { get; set; }
        public int PublishYear { get; set; }
        public int AvailableCopies { get; set; }

        public ICollection<AuthorRequestDto> Authors { get; set; } = new List<AuthorRequestDto>();
    }

    public class AuthorRequestDto
    {
        public string Name { get; set; }
        public int BirthYear { get; set; }

    }
}
