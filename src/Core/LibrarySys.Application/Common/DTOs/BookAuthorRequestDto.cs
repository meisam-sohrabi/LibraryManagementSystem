namespace LibrarySys.Application.Common.DTOs
{
    public class BookAuthorRequestDto 
    {
        public string Title { get; set; }
        public string Genere { get; set; }
        public int PublishYear { get; set; }
        public int AvailableCopies { get; set; }
        public ICollection<AuthorRequestAppDto> Authors { get; set; } = new List<AuthorRequestAppDto>();
    }

    public class AuthorRequestAppDto
    {
        public string Name { get; set; }
        public int BirthYear { get; set; }

    }
}
