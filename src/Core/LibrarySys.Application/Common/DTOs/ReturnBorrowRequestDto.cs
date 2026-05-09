namespace LibrarySys.Application.Common.DTOs
{
    public class ReturnBorrowRequestDto
    {
        public DateTime ReturnDate { get; set; }
        public Guid ReturnCode { get; set; }
    }
}
