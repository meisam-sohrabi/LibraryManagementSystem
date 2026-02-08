namespace LibrarySys.Domain.Entity
{
    public class Borrowing
    {
        public Guid Id { get; set; }
        public DateTime BorrowDate { get; set; } 
        public DateTime? ReturnDate { get; set; }

        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public Guid MemberId {  get; set; }
        public Member? Member { get; set; }
    }
}
