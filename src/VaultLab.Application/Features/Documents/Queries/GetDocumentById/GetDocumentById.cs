using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using VaultLab.Application.Abstractions;
using VaultLab.Application.DTOs.Documents;

namespace VaultLab.Application.Features.Documents.Queries.GetDocumentById
{
    public sealed record GetDocumentByIdQuery(
        Guid DocumentId
    ) : IRequest<DocumentResponse?>;

    public sealed class GetDocumentByIdHandler(IDocumentRepository documentRepository) : IRequestHandler<GetDocumentByIdQuery, DocumentResponse?>
    {
        public async Task<DocumentResponse?> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var document = await documentRepository.GetByIdAsync(
                request.DocumentId, cancellationToken
            );

            if (document is null)
                return null;

            return new DocumentResponse(
                document.Id,
                document.UserId,
                document.FileName,
                document.ContentType,
                document.FileSize,
                document.Status.ToString(),
                document.CreatedAt
            );
        }
    }
}