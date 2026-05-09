namespace LibrarySys.Application.Common.DTOs
{
    public class OTPRequestDto
    {
        public string mobile { get; set; }
        public int templateId { get; set; }
        public OTPParameter[] parameters { get; set; }

    }

    public class OTPParameter
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
