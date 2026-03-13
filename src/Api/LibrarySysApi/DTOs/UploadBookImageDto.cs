namespace LibrarySysApi.DTOs
{
    public class UploadBookImageDto
    {
        public Guid BookId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
