using System.Security.Claims;
using Microsoft.Extensions.Options;
using VaultLab.Application.Abstractions;
using VaultLab.Application.Features.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VaultLab.Infrastructure.Authentication
{
    public sealed class JwtAccessTokenService(IOptions<JwtOptions> jwtOptions) : IAccessTokenService
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;
        public AccessTokenResult GenerateToken(Guid userId, string email, string name)
        {
            var now = DateTime.UtcNow;

            var expiresAt = now.AddMinutes(_jwtOptions.AccessTokenExpirationMinutes);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.Email, email),
                new(JwtRegisteredClaimNames.Name, name),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: expiresAt,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return new AccessTokenResult(tokenString, expiresAt);
        }
    }
}