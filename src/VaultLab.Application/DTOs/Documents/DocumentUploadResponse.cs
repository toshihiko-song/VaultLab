using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.Application.DTOs.Documents
{
    public sealed record DocumentUploadResponse(
        Guid DocumentId
    );
}