using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Documents
{
    public sealed class DocumentTextExtractorFactory(IEnumerable<IDocumentTextExtractor> extractors) : IDocumentTextExtractorFactory
    {
        public IDocumentTextExtractor Create(string contentType)
        {
            return contentType.ToLowerInvariant() switch
            {
                "text/plain" => extractors.OfType<PlainTextDocumentTextExtractor>().Single(),
                "application/pdf" => extractors.OfType<PdfDocumentTextExtractor>().Single(),
                _ => throw new NotSupportedException($"Unsupported document type: {contentType}")
            };
        }
    }
}