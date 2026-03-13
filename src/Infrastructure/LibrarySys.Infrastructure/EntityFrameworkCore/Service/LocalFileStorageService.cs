using LibrarySys.Application.Contract.FileService;
using LibrarySys.Application.Option;
using Microsoft.Extensions.Options;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Service
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IOptions<FileStoragePathOption> _fileStorageOption;

        public LocalFileStorageService(IOptions<FileStoragePathOption> fileStorageOption)
        {
            _fileStorageOption = fileStorageOption;
        }
        public async Task<string> SaveBookImageAsync(Stream imageStream, string fileName)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), _fileStorageOption.Value.BookImagePath);
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            string fileNameGuid = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            string fullPath = Path.Combine(imagePath, fileNameGuid);
            using var fileStream = new FileStream(fullPath, FileMode.Create);
            await imageStream.CopyToAsync(fileStream);

            return $"/BookImages{fileNameGuid}";
        }
    }
}
