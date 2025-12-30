namespace LibrarySys.Domain.Entity
{
    public class Member
    {
        public Guid Id { get; set; } 
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }


        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
