using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Storage
{
    public class LocalFIleStorage : IFileStorage
    {
        public async Task<string> SaveAsync(Stream content, string filename, CancellationToken cancellationToken = default)
        {
            var storageDirectory = Path.Combine(AppContext.BaseDirectory, "storage");

            Directory.CreateDirectory(storageDirectory);


            var storageFileName = $"{Guid.NewGuid()}_{filename}";

            var filePath = Path.Combine(storageDirectory, storageFileName);

            await using var filestream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None
            );

            await content.CopyToAsync(filestream, cancellationToken);

            return filePath;
        }
    }
}