namespace LibrarySys.Domain.Entity
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genere { get; set; }
        public int PublishYear { get; set; }
        public int AvailableCopies { get; set; }

        public ICollection<BookAuthor> Authors { get; set; } = new List<BookAuthor>();
        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
