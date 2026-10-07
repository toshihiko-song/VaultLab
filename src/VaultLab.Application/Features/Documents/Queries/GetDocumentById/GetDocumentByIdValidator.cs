using FluentValidation;

namespace VaultLab.Application.Features.Documents.Queries.GetDocumentById
{
    public sealed class GetDocumentByIdValidator : AbstractValidator<GetDocumentByIdQuery>
    {
        public GetDocumentByIdValidator()
        {
            RuleFor(x => x.DocumentId)
                .NotEmpty()
                .WithMessage("Document ID is required");
        }
    }
}