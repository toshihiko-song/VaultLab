
namespace VaultLab.Application.Features.Authentication
{
    public sealed record ExternalUserInfo(
        string Provider,
        string ProviderUserId,
        string Email,
        string FirstName,
        string LastName
    );
}