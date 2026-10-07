using MediatR;
using VaultLab.Domain.Entities;
using VaultLab.Application.Abstractions;
using VaultLab.Application.Contracts.Messaging;
using VaultLab.Application.DTOs.Documents;

namespace VaultLab.Application.Features.Documents.Commands.UploadDocument
{
    public sealed record UploadDocumentCommand(
        Guid UserId,
        string FileName,
        string ContentType,
        long FileSize,
        Stream Content
    ) : IRequest<DocumentUploadResponse>;

    public sealed class UploadDocumentHandler(IDocumentRepository documentRepository, IFileStorage fileStorage, IMessagePublisher messagePublisher) : IRequestHandler<UploadDocumentCommand, DocumentUploadResponse>
    {

        public async Task<DocumentUploadResponse> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            var storagePath = await fileStorage.SaveAsync(
                request.Content,
                request.FileName,
                cancellationToken
            );

            var document = new Document(
                request.UserId,
                request.FileName,
                request.ContentType,
                request.FileSize,
                storagePath
            );

            await documentRepository.AddAsync(
                document,
                cancellationToken
            );

            await documentRepository.SaveChangesAsync(cancellationToken);


            await messagePublisher.PublishMessage(new DocumentUploadMessage(document.Id, document.UserId), cancellationToken);


            return new DocumentUploadResponse(document.Id);
        }
    }
}