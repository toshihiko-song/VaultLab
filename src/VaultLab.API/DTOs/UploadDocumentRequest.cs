using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultLab.API.DTOs
{
    public class UploadDocumentRequest
    {
        public IFormFile File { get; set; } = null!;
    }
}