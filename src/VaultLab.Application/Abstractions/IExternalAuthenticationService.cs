using VaultLab.Application.Features.Authentication;

namespace VaultLab.Application.Abstractions
{
    public interface IExternalAuthenticationService
    {
        Task<ExternalUserInfo> AuthenticateAsync(
            string idToken,
            CancellationToken cancellationToken
        );
    }
}