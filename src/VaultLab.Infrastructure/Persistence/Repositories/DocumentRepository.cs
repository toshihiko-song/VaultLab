using VaultLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Persistence.Repositories
{
    public sealed class DocumentRepository(VaultLabDbContext dbContext) : IDocumentRepository
    {
        public async Task AddAsync(Document document, CancellationToken cancellationToken = default)
        {
            await dbContext.Documents.AddAsync(document, cancellationToken);
        }

        public async Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Documents
                .Include(x => x.Chunks)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken
                );
        }

        public async Task<IReadOnlyList<Document>> GetDocumentsByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await dbContext.Documents
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}