
namespace VaultLab.Application.DTOs.Documents
{
    public sealed record DocumentResponse(
        Guid Id,
        Guid UserId,
        string FileName,
        string ContentType,
        long FileSize,
        string Status,
        DateTime CreatedAt
    );
}