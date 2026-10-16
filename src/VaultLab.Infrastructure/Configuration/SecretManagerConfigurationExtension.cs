using Amazon;
using System.Text.Json;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;

namespace VaultLab.Infrastructure.Configuration
{
    public static class SecretManagerConfigurationExtension
    {
        public static async Task AddSecretsManagerAsync(
        this IConfigurationBuilder configuration,
        string region,
        IEnumerable<string> secretNames)
        {
            using var client = new AmazonSecretsManagerClient(
                RegionEndpoint.GetBySystemName(region));

            var values = new Dictionary<string, string?>(
                StringComparer.OrdinalIgnoreCase);

            foreach (var secretName in secretNames)
            {

                var response = await client.GetSecretValueAsync(
                    new GetSecretValueRequest
                    {
                        SecretId = secretName
                    });

                if (string.IsNullOrWhiteSpace(response.SecretString))
                {
                    throw new InvalidOperationException(
                        $"Secret '{secretName}' has no string value.");
                }

                using var document = JsonDocument.Parse(response.SecretString);

                if (document.RootElement.ValueKind != JsonValueKind.Object)
                {
                    throw new InvalidOperationException(
                        $"Secret '{secretName}' must contain a JSON object.");
                }

                foreach (var property in document.RootElement.EnumerateObject())
                {
                    var value = property.Value.ValueKind switch
                    {
                        JsonValueKind.String => property.Value.GetString(),
                        JsonValueKind.Null => null,
                        _ => property.Value.ToString()
                    };

                    values[$"{secretName}:{property.Name}"] = value;
                }
            }

            configuration.AddInMemoryCollection(values);
        }
    }
}