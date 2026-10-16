namespace VaultLab.Infrastructure.Configuration
{
    public sealed class OpenAISettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string EmbeddingModel { get; set; } = "text-embedding-3-small";
        public string ChatModel { get; set; } = "gpt-4.1-mini";
    }
}