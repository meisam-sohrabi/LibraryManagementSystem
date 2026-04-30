namespace LibrarySys.Domain.Entity
{
    public class BookAuthor : BaseClass
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
