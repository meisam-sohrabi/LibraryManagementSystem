namespace LibrarySys.Application.Common.DTOs
{
    public class PermissionDto
    {
        public string Resource { get; set; }
        public string Action { get; set; }
        public string? Description { get; set; }
    }
}
