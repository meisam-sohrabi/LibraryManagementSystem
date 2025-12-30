using System.Text.Json.Serialization;

namespace LibrarySys.Domain.Entity
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BirthYear {  get; set; }

        public ICollection<BookAuthor> Books { get; set; } = new List<BookAuthor>();
    }
}
