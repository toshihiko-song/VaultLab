
using VaultLab.Domain.Entities;
using VaultLab.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace VaultLab.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(VaultLabDbContext dbContext, CancellationToken cancellationToken = default)
        {
            if (await dbContext.Users.AnyAsync(cancellationToken))
                return;

            var user = new User(
                new Email("test@vaultLab.com"),
                "Test User"
            );

            dbContext.Users.Add(user);


            await dbContext.SaveChangesAsync(cancellationToken);

        }
        
    }
}