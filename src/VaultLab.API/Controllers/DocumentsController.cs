using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaultLab.Application.Features.Documents.Commands.UploadDocument;

namespace VaultLab.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController(ISender sender): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            if(file.Length == 0)
                return BadRequest("File is empty.");

            using var stream = file.OpenReadStream();

            var command = new UploadDocumentCommand(
                Guid.Parse("98231833-da05-44c6-93d9-d55493b6f466"), // TODO  replace this once authentication is implemented
                file.FileName,
                file.ContentType,
                file.Length,
                stream
            );


            var documentId = await sender.Send(
                command,
                cancellationToken
            );

            return Ok(documentId);
        }
        
    }
}