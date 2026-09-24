using VaultLab.Domain.Entities;

namespace VaultLab.Application.Abstractions
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task AddAsync(
            Document document,
            CancellationToken cancellationToken = default
        );

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default
        );
    }
}