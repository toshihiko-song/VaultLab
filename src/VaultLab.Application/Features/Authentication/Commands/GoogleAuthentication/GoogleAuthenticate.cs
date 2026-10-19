using MediatR;
using VaultLab.Application.Abstractions;
using VaultLab.Application.Features.Authentication.Models;
using VaultLab.Domain.Entities;
using VaultLab.Domain.ValueObjects;

namespace VaultLab.Application.Features.Authentication.Commands.GoogleAuthentication
{

    public sealed record GoogleAuthenticateCommand(
        string IdToken
    ) : IRequest<AuthenticationResult>;

    public sealed class GoogleAuthenticateHandler(
        IExternalAuthenticationService externalAuthenticationService,
        IUserRepository userRepository,
        IAccessTokenService accessTokenService,
        IRefreshTokenService refreshTokenService
    ) : IRequestHandler<GoogleAuthenticateCommand, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(GoogleAuthenticateCommand request, CancellationToken cancellationToken)
        {
            var externalUser = await externalAuthenticationService.AuthenticateAsync(request.IdToken, cancellationToken);

            var user = await userRepository.GetByEmailAsync(externalUser.Email, cancellationToken);

            //Create new user if not existing in db
            if (user is null)
            {
                var email = new Email(externalUser.Email);
                var name = $"{externalUser.FirstName} {externalUser.LastName}".Trim();

                user = new User(email, name);

                await userRepository.AddAsync(user, cancellationToken);

                await userRepository.SaveChangesAsync(cancellationToken);
            }

            var accessToken = accessTokenService.GenerateToken(user.Id, user.Email.Value, user.Name);

            var refreshToken = await refreshTokenService.CreateAsync(user.Id, cancellationToken);

            return new AuthenticationResult(
                accessToken.Token,
                refreshToken.Token,
                accessToken.ExpiresAt,
                refreshToken.ExpiresAt
            );


        }
    }
}