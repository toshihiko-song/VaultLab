
using MediatR;
using VaultLab.Domain.Entities;
using VaultLab.Application.Abstractions;

namespace VaultLab.Application.Features.Documents.Queries.GetUserDocuments
{
    public sealed record GetUserDocumentsQuery(
        Guid UserId
    ): IRequest<IReadOnlyList<Document>>;

    public sealed class GetUserDocumentsHandler(IDocumentRepository documentRepository) : IRequestHandler<GetUserDocumentsQuery, IReadOnlyList<Document>>
    {
        public async Task<IReadOnlyList<Document>> Handle(GetUserDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await documentRepository.GetDocumentsByUserId(
                request.UserId,
                cancellationToken);
        }
    }
}