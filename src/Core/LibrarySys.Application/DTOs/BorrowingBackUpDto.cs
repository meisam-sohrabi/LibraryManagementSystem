namespace LibrarySys.Application.DTOs
{
    public class BorrowingBackUpDto
    {
        public BackUpMeta Meta { get; set; }
        public List<BorrowingDto> Borrowings { get; set; }
    }

    public class BorrowingDto
    {
        public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }

    public class BackUpMeta
    {
        public Guid MemberId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
