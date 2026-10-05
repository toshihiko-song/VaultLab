using System.ComponentModel.DataAnnotations.Schema;
using Pgvector;

namespace VaultLab.Infrastructure.Persistence.Models
{
    [Table("DocumentChunks")]
    public class DocumentChunkModel
    {
            public Guid Id { get; set; }

        public Guid DocumentId { get; set; }

        public string Content { get; set; } = string.Empty;

        public int ChunkIndex { get; set; }

        [Column(TypeName = "vector(1536)")]
        public Vector? Embedding { get; set; }
    }
}