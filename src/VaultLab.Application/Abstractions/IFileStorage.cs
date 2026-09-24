namespace VaultLab.Application.Abstractions
{
    public interface IFileStorage
    {
        Task<string> SaveAsync(
            Stream content,
            string filename,
            CancellationToken cancellationToken = default
        );
    }
}