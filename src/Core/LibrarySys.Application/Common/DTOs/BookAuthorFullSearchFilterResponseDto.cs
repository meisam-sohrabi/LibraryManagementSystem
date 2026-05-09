namespace LibrarySys.Application.Common.DTOs
{
    public class BookAuthorFullSearchFilterResponseDto
    {
        public string AuthorName { get; set; }
        public int BirthYear { get; set; }
        public string Genere { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
    }
}
