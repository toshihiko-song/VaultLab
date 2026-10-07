using FluentValidation;

namespace VaultLab.Application.Features.Documents.Commands.UploadDocument
{
    public sealed class UploadDocumentValidator : AbstractValidator<UploadDocumentCommand>
    {
        public UploadDocumentValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");

            RuleFor(x => x.FileName)
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("File Name is required and must not exceed 255 characters");

            RuleFor(x => x.ContentType)
                .NotEmpty()
                .WithMessage("Content Type is required.");

            RuleFor(x => x.FileSize)
                .GreaterThan(0)
                .WithMessage("File Size must be greater than zero");

            RuleFor(x => x.Content)
                .NotNull()
                .WithMessage("File Content is required.");
        }
    }
}