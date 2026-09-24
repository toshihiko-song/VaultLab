using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaultLab.Application.Abstractions;
using VaultLab.Domain.Entities;

namespace VaultLab.Infrastructure.Persistence
{
    public class DocumentRepository : IDocumentRepository
    {
        public Task AddAsync(Document document, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}