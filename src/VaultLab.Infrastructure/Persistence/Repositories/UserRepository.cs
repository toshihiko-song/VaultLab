using VaultLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using VaultLab.Application.Abstractions;
using VaultLab.Domain.ValueObjects;

namespace VaultLab.Infrastructure.Persistence.Repositories
{
    public class UserRepository(VaultLabDbContext dbContext) : IUserRepository
    {
        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await dbContext.Users.AddAsync(user, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();

            return await dbContext.Users
                .FirstOrDefaultAsync(
                    user => user.Email == new Email(normalizedEmail),
                    cancellationToken
                );
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}