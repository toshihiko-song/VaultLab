using MediatR;
using VaultLab.Domain.Entities;
using VaultLab.Application.Abstractions;

namespace VaultLab.Application.Features.Documents.Commands.UploadDocument
{
    public sealed record UploadDocumentCommand(
        Guid UserId,
        string FileName,
        string ContentType,
        long FileSize,
        Stream Content
    ): IRequest<Guid>;

    public sealed class UploadDocumentHandler(IDocumentRepository documentRepository, IFileStorage fileStorage) : IRequestHandler<UploadDocumentCommand, Guid>
    {

        public async Task<Guid> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
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

            return document.Id;
        }
    }
}