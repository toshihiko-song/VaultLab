using VaultLab.Domain.Enums;

namespace VaultLab.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public string FileName { get; private set; } = string.Empty;

        public string ContentType { get; private set; } = string.Empty;

        public long FileSize { get; private set; }

        public string StoragePath { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; private set; }
        public DocumentStatus Status { get; private set; }
        public ICollection<DocumentChunk> Chunks { get; private set; }

        public Document(
            Guid userId,
            string fileName,
            string contentType,
            long fileSize,
            string storagePath)
        {
            if(userId == Guid.Empty)
                throw new ArgumentException("User Id is required.", nameof(userId));
            
            if(string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name is required.", nameof(fileName));

            if(string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentException("Content type is required.", nameof(contentType));

            if(fileSize <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(fileSize),
                    "File Size must be greater than zero."
                );
            
            if(string.IsNullOrWhiteSpace(storagePath))
                throw new ArgumentException("Storage path is required.", nameof(storagePath));
            
            Id = Guid.NewGuid();
            UserId = userId;
            FileName = fileName;
            ContentType = contentType;
            FileSize = fileSize;
            StoragePath = storagePath;
            CreatedAt = DateTime.UtcNow;
            Status = DocumentStatus.Uploaded;
            Chunks = [];
        }

        public void MarkAsProcessing()
        {
            Status = DocumentStatus.Processing;
        }

        public void MarkAsProcessed()
        {
            if(Status != DocumentStatus.Uploaded)
                throw new InvalidOperationException(
                    "Document must be in processing before it can be marked as processed"
                );
            
            Status = DocumentStatus.Proccesed;
        }

        public void MarkAsFailed()
        {
            Status = DocumentStatus.Failed;
        }
    }
}