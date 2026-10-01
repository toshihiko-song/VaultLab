using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaultLab.Application.Abstractions;

namespace VaultLab.Infrastructure.Documents
{
    public class SimpleDocumentChunker: IDocumentChunker
    {
        private const int ChunkSize = 1000;

        public IReadOnlyList<string> Chunk(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return [];

            var chunks = new List<string>();

            for (var i = 0; i < text.Length; i += ChunkSize)
            {
                var length = Math.Min(
                    ChunkSize,
                    text.Length - i);

                chunks.Add(
                    text.Substring(i, length));
            }

            return chunks;
        }
    }
}