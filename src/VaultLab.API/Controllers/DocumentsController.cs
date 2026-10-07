using MediatR;
using VaultLab.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using VaultLab.Application.DTOs.Documents;
using VaultLab.Application.Features.Documents.Queries.AskQuestion;
using VaultLab.Application.Features.Documents.Queries.GetDocumentById;
using VaultLab.Application.Features.Documents.Commands.UploadDocument;
using VaultLab.Application.Features.Documents.Queries.GetUserDocuments;

namespace VaultLab.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload(
            [FromForm] UploadDocumentRequest request,
            CancellationToken cancellationToken
        )
        {
            using var stream = request.File.OpenReadStream();

            var command = new UploadDocumentCommand(
                Guid.Parse("98231833-da05-44c6-93d9-d55493b6f466"), // TODO  replace this once authentication is implemented
                request.File.FileName,
                request.File.ContentType,
                request.File.Length,
                stream
            );


            var response = await sender.Send(
                command,
                cancellationToken
            );

            return Ok(response);
        }

        [HttpGet("users/{userId}")]
        public async Task<ActionResult<IReadOnlyList<DocumentResponse>>> GetUserDocuments(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserDocumentsQuery(userId);

            var documents = await sender.Send(query, cancellationToken);

            return Ok(documents);
        }

        [HttpPost("ask")]
        public async Task<ActionResult<AskQuestionResponse>> Ask(
            [FromBody] AskQuestionQuery query,
            CancellationToken cancellationToken
        )
        {
            var response = await sender.Send(query, cancellationToken);

            return Ok(response);
        }


        [HttpGet("{documentId:guid}")]
        public async Task<IActionResult> GetDocumentById(
            [FromRoute] Guid documentId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetDocumentByIdQuery(documentId);

            var document = await sender.Send(query, cancellationToken);

            if (document is null)
                return NotFound();

            return Ok(document);
        }
    }
}