
using MediatR;
using VaultLab.Application.Abstractions;
using VaultLab.Application.Features.Documents.Models;

namespace VaultLab.Application.Features.Documents.Queries.AskQuestion
{
    public sealed record AskQuestionQuery(
        string Question,
        int Limit = 5
    ) : IRequest<AskQuestionResponse>;

    public sealed record AskQuestionResponse(
        string Aswer,
        IReadOnlyList<SimilarChunk> Sources
    );

    public sealed class AnswerQuestionHandler(
        IDocumentRepository documentRepository,
        IEmbeddingGenerator embeddingGenerator,
        ILanguageModel languageModel
    ) : IRequestHandler<AskQuestionQuery, AskQuestionResponse>
    {
        public async Task<AskQuestionResponse> Handle(AskQuestionQuery request, CancellationToken cancellationToken)
        {
            var embedding = await embeddingGenerator.GenerateAsync(request.Question, cancellationToken);

            var chunks = await documentRepository.SearchSimilarChunksAsync(
                embedding,
                request.Limit,
                cancellationToken
            );

            var relevantChunks = chunks
                .Where(x => x.Similarity >= 0.25)
                .ToList();


            if (relevantChunks.Count == 0)
            {
                return new AskQuestionResponse(
                    "I don't have enough information to answer that question based on the available documents.",
                    []
                );
            }

            var context = string.Join("\n\n---\n\n", chunks.Select((chunk, index) => $"Source {index + 1}: \n{chunk.Content}"));


            var prompt = $"""
            You are a helpful assistant answering questions based only on the provided document context.

            Rules:
            - Use only the information provided in the context.
            - If the answer cannot be found in the context, say that you don't have enough information.
            - Do not make up or assume information.
            - Give a clear and concise answer.

            Context:
            {context}

            Question:
            {request.Question}
            """;

            var answer = await languageModel.GenerateAsync(prompt, cancellationToken);

            return new AskQuestionResponse(answer, chunks);
        }
    }
}