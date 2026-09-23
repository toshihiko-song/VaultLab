namespace VaultLab.Domain.Entities
{
    public class DocumentChunk
    {
        public Guid Id { get; private set; }
        public Guid DocumentId { get; private set; }
        public string Content { get; private set; } = string.Empty;
        public int ChunkIndex { get; private set; }

        public DocumentChunk(
            Guid documentId,
            string content,
            int chunkIndex)
        {
            if(documentId == Guid.Empty)
                throw new ArgumentException("Document Id is required", nameof(documentId));
            
            if(string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content is required", nameof(content));

            if(chunkIndex < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(chunkIndex),
                    "Chunk index cannot be negative."
                );
            
            Id = Guid.NewGuid();
            DocumentId = documentId;
            Content = content;
            ChunkIndex = chunkIndex;
        }
    }
}