using VaultLab.Application.Features.Authentication.Models;

namespace VaultLab.Application.Abstractions
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenResult> CreateAsync(
            Guid userId,
            CancellationToken cancellationToken = default
        );
    }
}