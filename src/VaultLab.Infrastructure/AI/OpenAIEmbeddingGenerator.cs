using OpenAI.Embeddings;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.AI
{
    public sealed class OpenAIEmbeddingGenerator(EmbeddingClient client) : IEmbeddingGenerator
    {
        public async Task<IReadOnlyList<float>> GenerateAsync(string text, CancellationToken cancellationToken = default)
        {
            var embedding = await client.GenerateEmbeddingAsync(input: text, cancellationToken: cancellationToken);

            return embedding.Value.ToFloats().ToArray();

        }
    }
}