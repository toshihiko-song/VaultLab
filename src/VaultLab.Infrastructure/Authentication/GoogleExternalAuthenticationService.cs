using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using VaultLab.Application.Abstractions;
using VaultLab.Application.Features.Authentication;


namespace VaultLab.Infrastructure.Authentication
{
    public sealed class GoogleExternalAuthenticationService(IOptions<GoogleAuthenticationOptions> authOptions) : IExternalAuthenticationService
    {
        public async Task<ExternalUserInfo> AuthenticateAsync(string idToken, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = [authOptions.Value.ClientId]
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(
                idToken, settings
            );

            cancellationToken.ThrowIfCancellationRequested();

            return new ExternalUserInfo(
                Provider: "Google",
                ProviderUserId: payload.Subject,
                Email: payload.Email,
                FirstName: payload.GivenName ?? string.Empty,
                LastName: payload.FamilyName ?? string.Empty
            );
        }
    }
}