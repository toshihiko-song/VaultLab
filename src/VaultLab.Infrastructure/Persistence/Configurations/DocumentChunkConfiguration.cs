using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaultLab.Domain.Entities;

namespace VaultLab.Infrastructure.Persistence.Configurations
{
    public sealed class DocumentChunkConfiguration : IEntityTypeConfiguration<DocumentChunk>
    {
        public void Configure(EntityTypeBuilder<DocumentChunk> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ChunkIndex)
                .IsRequired();

            builder.Property(x => x.Content)
                .IsRequired();

            builder.HasOne<Document>()
                .WithMany(x => x.Chunks)
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(
                x => new
                {
                    x.DocumentId,
                    x.ChunkIndex
                }
            )
            .IsUnique();


        }
    }
}