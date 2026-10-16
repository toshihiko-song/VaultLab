using DotNetEnv;

namespace VaultLab.IntegrationTests
{
    public static class TestEnvironment
    {
        private static readonly bool Initialized = Initialize();

        public static void EnsureInitialized()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("Test environment initialization failed.");
            }
        }

        private static bool Initialize()
        {
            const string variableName = "ConnectionStrings__VaultLab_Test";

            // CI/CD may provide the connection string directly.
            if (!string.IsNullOrWhiteSpace(
                    Environment.GetEnvironmentVariable(variableName)))
            {
                return true;
            }

            // Local development: search for .env.
            var directory = Directory.GetCurrentDirectory();

            while (directory is not null)
            {
                var envPath = Path.Combine(directory, ".env");

                if (File.Exists(envPath))
                {
                    Env.Load(envPath);
                    return true;
                }

                directory = Directory.GetParent(directory)?.FullName;
            }

            throw new InvalidOperationException(
                $"Set the {variableName} environment variable or provide a local .env file.");

        }

    }
}