namespace VaultLab.Application.Contracts.Messaging
{
    public sealed record DocumentUploadMessage(
        Guid DocumentId,
        Guid UserId
    );
}