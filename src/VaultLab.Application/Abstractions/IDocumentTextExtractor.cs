using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Application.Abstractions
{
    public interface IDocumentTextExtractor
    {
        Task<string> ExtractAsync(
            Stream content,
            string contentType,
            CancellationToken cancellationToken = default
        );
    }
}