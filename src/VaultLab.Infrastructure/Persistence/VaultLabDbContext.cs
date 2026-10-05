using Microsoft.EntityFrameworkCore;
using VaultLab.Domain.Entities;
using VaultLab.Infrastructure.Persistence.Models;

namespace VaultLab.Infrastructure.Persistence
{
    public class VaultLabDbContext : DbContext
    {
        public VaultLabDbContext(DbContextOptions<VaultLabDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<DocumentChunkModel> DocumentChunks => Set<DocumentChunkModel>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("vector");

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(VaultLabDbContext).Assembly
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}