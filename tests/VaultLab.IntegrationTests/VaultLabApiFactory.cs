using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace VaultLab.IntegrationTests
{
    public sealed class VaultLabApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureAppConfiguration((_, config) =>
            {

                TestEnvironment.EnsureInitialized();

                var testConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__VaultLab_Test");

                if (string.IsNullOrWhiteSpace(testConnectionString))
                {
                    throw new InvalidOperationException(
                        "The environment variable 'ConnectionStrings__VaultLab_Test' is not set. " +
                        "Please set it to the connection string for the test database."
                    );
                }

                config.AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["ConnectionStrings:VaultLab"] = testConnectionString
                    }
                );
            });
        }
    }
}