using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Application.Abstractions
{
    public interface IEmbeddingGenerator
    {
        Task<IReadOnlyList<float>> GenerateAsync
        (
            string text,
            CancellationToken cancellationToken = default
        );
    }
}