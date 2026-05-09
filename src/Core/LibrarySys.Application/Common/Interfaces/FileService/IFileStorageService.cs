namespace LibrarySys.Application.Common.Interfaces.FileService
{
    public interface IFileStorageService
    {
        Task<string> SaveBookImageAsync(Stream imageStream, string fileName);
    }
}
