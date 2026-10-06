using DocumentFormat.OpenXml.Presentation;
using OpenAI.Chat;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.AI
{
    public sealed class OpenAILanguageModel(ChatClient client) : ILanguageModel
    {
        public async Task<string> GenerateAsync(string prompt, CancellationToken cancellationToken = default)
        {
            var response = await client.CompleteChatAsync(messages: [prompt], cancellationToken: cancellationToken);

            return response.Value.Content[0].Text;
        }
    }
}