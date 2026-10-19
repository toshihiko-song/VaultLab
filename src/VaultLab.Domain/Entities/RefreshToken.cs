namespace VaultLab.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string TokenHash { get; private set; } = string.Empty;
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }

        public User User { get; private set; } = null!;

        private RefreshToken()
        {

        }

        public RefreshToken(
            Guid userId,
            string token,
            DateTime expiresAt
        )
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Token is required.", nameof(token));
            }

            if (expiresAt <= DateTime.Now)
            {
                throw new ArgumentException("Expiration must be in the future", nameof(expiresAt));
            }


            Id = Guid.NewGuid();
            UserId = userId;
            TokenHash = token;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.UtcNow;
        }

        public void Revoke()
        {
            if (RevokedAt.HasValue)
                return;

            RevokedAt = DateTime.UtcNow;
        }

        public bool IsExpired =>
            DateTime.UtcNow >= ExpiresAt;


        public bool IsRevoked =>
            RevokedAt.HasValue;

        public bool IsActive =>
            !IsExpired && !IsRevoked;

    }
}