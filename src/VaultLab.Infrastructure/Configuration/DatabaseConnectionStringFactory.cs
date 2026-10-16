using Npgsql;

namespace VaultLab.Infrastructure.Configuration
{
    public static class DatabaseConnectionStringFactory
    {
        public static string Create(DatabaseSettings settings)
        {
            return new NpgsqlConnectionStringBuilder
            {
                Host = settings.Host,
                Port = settings.Port,
                Database = settings.Database,
                Username = settings.Username,
                Password = settings.Password
            }.ConnectionString;
        }
    }
}