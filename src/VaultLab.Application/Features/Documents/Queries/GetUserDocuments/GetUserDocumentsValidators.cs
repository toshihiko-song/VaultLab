
using FluentValidation;

namespace VaultLab.Application.Features.Documents.Queries.GetUserDocuments
{
    public sealed class GetUserDocumentsValidators : AbstractValidator<GetUserDocumentsQuery>
    {
        public GetUserDocumentsValidators()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User id is required");
        }
    }
}