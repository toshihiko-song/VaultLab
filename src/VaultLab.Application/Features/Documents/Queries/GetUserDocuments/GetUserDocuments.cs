
using MediatR;
using VaultLab.Domain.Entities;
using VaultLab.Application.Abstractions;
using VaultLab.Application.DTOs.Documents;

namespace VaultLab.Application.Features.Documents.Queries.GetUserDocuments
{
    public sealed record GetUserDocumentsQuery(
        Guid UserId
    ) : IRequest<IReadOnlyList<DocumentResponse>>;

    public sealed class GetUserDocumentsHandler(IDocumentRepository documentRepository) : IRequestHandler<GetUserDocumentsQuery, IReadOnlyList<DocumentResponse>>
    {
        public async Task<IReadOnlyList<DocumentResponse>> Handle(GetUserDocumentsQuery request, CancellationToken cancellationToken)
        {
            var documents = await documentRepository.GetDocumentsByUserId(
                request.UserId,
                cancellationToken);

            return [.. documents
                .Select(doc => new DocumentResponse(
                    doc.Id,
                    doc.UserId,
                    doc.FileName,
                    doc.ContentType,
                    doc.FileSize,
                    doc.Status.ToString(),
                    doc.CreatedAt
                ))];
        }
    }
}