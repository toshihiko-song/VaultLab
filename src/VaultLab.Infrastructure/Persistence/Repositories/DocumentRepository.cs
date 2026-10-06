using VaultLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using VaultLab.Application.Abstractions;
using VaultLab.Infrastructure.Persistence.Models;
using Pgvector.EntityFrameworkCore;
using VaultLab.Application.Features.Documents.Models;

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

        public async Task AddChunkAsync(
            IEnumerable<DocumentChunk> chunks,
            IReadOnlyList<float[]> embeddings,
            CancellationToken cancellationToken = default)
        {

            var chunkList = chunks.ToList();

            if (chunkList.Count != embeddings.Count)
                throw new ArgumentException(
                    "The number of chunks must match the number of embeddings.",
                    nameof(embeddings)
                );


            var models = chunkList
            .Select((chunk, index) => new DocumentChunkModel
            {
                Id = chunk.Id,
                DocumentId = chunk.DocumentId,
                Content = chunk.Content,
                ChunkIndex = chunk.ChunkIndex,
                Embedding = new Pgvector.Vector(embeddings[index])
            })
            .ToList();

            await dbContext.Set<DocumentChunkModel>()
                .AddRangeAsync(models, cancellationToken);
        }

        public async Task<IReadOnlyList<SimilarChunk>> SearchSimilarChunksAsync(IReadOnlyList<float> embedding, int limit, CancellationToken cancellationToken = default)
        {

            var vector = new Pgvector.Vector(embedding.ToArray());


            return await dbContext.DocumentChunks
                .Where(x => x.Embedding != null)
                .OrderBy(x => x.Embedding!.CosineDistance(vector))
                .Take(limit)
                .Select(x => new SimilarChunk(
                    x.DocumentId,
                    x.Content,
                    x.ChunkIndex,
                    1 - x.Embedding!.CosineDistance(vector)
                ))
                .ToListAsync(cancellationToken);
        }
    }
}