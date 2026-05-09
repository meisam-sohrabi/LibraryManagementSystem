

namespace LibrarySys.Domain.Entity
{
    public class Borrowing : BaseClass
    {
        public Guid Id { get; set; }
        public DateTime BorrowDate { get; set; } 
        public DateTime? ReturnDate { get; set; }
        public Guid ReturnCode { get; set; }
        public int MaxBorrowDay { get; set; }   // تعداد روز های مجاز قرض کتاب
        public int? lateFee { get; set; }  // جریمه روزانه
        public int? TotalLateFee { get; set; } // مجموع جریمه

        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public Guid MemberId {  get; set; }
        public Member? Member { get; set; }
    }
}
