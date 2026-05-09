namespace LibrarySys.Application.Common.DTOs
{
    public class OTPResponseDto
    {
        public int status { get; set; }
        public string message { get; set; }
        public data data { get; set; }
    }

    public class data
    {
        public int messageId { get; set; }
        public decimal cost { get; set; }

    }
}
