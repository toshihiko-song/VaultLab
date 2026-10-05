using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Application.Abstractions
{
    public interface IDocumentTextExtractorFactory
    {
        IDocumentTextExtractor Create(string contentType);
    }
}