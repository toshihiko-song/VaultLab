using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Application.Features.Documents.Models
{
    public sealed record SimilarChunk(
        Guid DocumentId,
        string Content,
        int ChunkIndex,
        double Similarity
    );
}