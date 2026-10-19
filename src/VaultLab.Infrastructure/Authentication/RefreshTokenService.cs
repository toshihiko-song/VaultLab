using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using VaultLab.Application.Abstractions;
using VaultLab.Application.Features.Authentication.Models;
using VaultLab.Domain.Entities;
using VaultLab.Infrastructure.Persistence;

namespace VaultLab.Infrastructure.Authentication
{
    public sealed class RefreshTokenService(VaultLabDbContext dbContext) : IRefreshTokenService
    {
        private const int TokenLifeTimeDays = 5;

        public async Task<RefreshTokenResult> CreateAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var token = WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(64));

            var tokenHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

            var expiresAt = DateTime.UtcNow.AddDays(TokenLifeTimeDays);

            var refreshToken = new RefreshToken(userId, tokenHash, expiresAt);

            await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);

            return new RefreshTokenResult(token, expiresAt);
        }
    }
}