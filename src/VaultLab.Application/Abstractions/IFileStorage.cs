namespace VaultLab.Application.Abstractions
{
    public interface IFileStorage
    {
        Task<string> SaveAsync(
            Stream content,
            string filename,
            CancellationToken cancellationToken = default
        );


        Task<Stream> OpenReadAsync(
            string storagePath,
            CancellationToken cancellationToken = default
        );
    }
}