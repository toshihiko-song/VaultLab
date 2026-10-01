
using System.Text;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Documents
{
    public class PlainTextDocumentTextExtractor : IDocumentTextExtractor
    {
        public async Task<string> ExtractAsync(Stream content, string contentType, CancellationToken cancellationToken = default)
        {
            using var reader = new StreamReader(
                content,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                leaveOpen: true
            );

            return await reader.ReadToEndAsync(cancellationToken);
        }
    }
}