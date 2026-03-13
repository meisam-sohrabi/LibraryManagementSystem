namespace LibrarySys.Application.Contract.FileService
{
    public interface IFileStorageService
    {
        Task<string> SaveBookImageAsync(Stream imageStream, string fileName);
    }
}
