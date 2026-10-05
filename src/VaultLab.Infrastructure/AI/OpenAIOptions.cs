using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Infrastructure.AI
{
    public class OpenAIOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string  EmbeddingModel { get; set; } = "text-embedding-3-small";
    }
}