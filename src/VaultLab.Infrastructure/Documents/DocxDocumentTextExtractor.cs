using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Documents
{
    public sealed class DocxDocumentTextExtractor : IDocumentTextExtractor
    {
        public Task<string> ExtractAsync(Stream content, string contentType, CancellationToken cancellationToken = default)
        {
            using var document = WordprocessingDocument.Open(
           content,
           false);

            var mainDocumentPart = document.MainDocumentPart;

            if (mainDocumentPart is null)
                return Task.FromResult(string.Empty);

            var wordDocument = mainDocumentPart.Document;

            if (wordDocument is null)
                return Task.FromResult(string.Empty);

            var body = wordDocument.Body;

            if (body is null)
                return Task.FromResult(string.Empty);

            var text = string.Join(
                Environment.NewLine,
                body
                    .Descendants<Text>()
                    .Select(x => x.Text));

            return Task.FromResult(text);
        }
    }
}