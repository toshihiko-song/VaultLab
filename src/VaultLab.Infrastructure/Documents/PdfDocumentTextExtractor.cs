using UglyToad.PdfPig;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Documents
{
    public sealed class PdfDocumentTextExtractor : IDocumentTextExtractor
    {
        public Task<string> ExtractAsync(Stream content, string contentType, CancellationToken cancellationToken = default)
        {
            using var document = PdfDocument.Open(content);

            var text = string.Join(
                Environment.NewLine,
                document.GetPages()
                    .Select(page => page.Text)
            );

            return Task.FromResult(text);
        }
    }
}