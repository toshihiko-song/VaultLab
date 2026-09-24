using Microsoft.EntityFrameworkCore;
using VaultLab.Domain.Entities;

namespace VaultLab.Infrastructure.Persistence
{
    public class VaultLabDbContext: DbContext
    {
        public VaultLabDbContext(DbContextOptions<VaultLabDbContext> options): base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<DocumentChunk> DocumentChunks => Set<DocumentChunk>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(VaultLabDbContext).Assembly
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}