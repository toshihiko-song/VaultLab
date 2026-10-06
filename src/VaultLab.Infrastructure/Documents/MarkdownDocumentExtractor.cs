using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Documents
{
    public sealed class MarkdownDocumentExtractor : IDocumentTextExtractor
    {
        public async Task<string> ExtractAsync(Stream content, string contentType, CancellationToken cancellationToken = default)
        {
            using var reader = new StreamReader(content, leaveOpen: true);

            return await reader.ReadToEndAsync(cancellationToken);
        }
    }
}