using VaultLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using VaultLab.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VaultLab.Infrastructure.Persistence.Configurations
{
    public sealed class DocumentChunkModelConfiguration : IEntityTypeConfiguration<DocumentChunkModel>
    {
        public void Configure(EntityTypeBuilder<DocumentChunkModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.ChunkIndex)
                .IsRequired();

            builder.Property(x => x.Embedding)
                .HasColumnType("vector(1536)");

            builder.HasIndex(x => new
            {
                x.DocumentId,
                x.ChunkIndex
            })
            .IsUnique();

            builder.HasOne<Document>()
                .WithMany()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}