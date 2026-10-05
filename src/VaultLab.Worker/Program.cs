using OpenAI.Embeddings;
using VaultLab.Worker.Messaging;
using VaultLab.Infrastructure.AI;
using Microsoft.EntityFrameworkCore;
using VaultLab.Infrastructure.Storage;
using VaultLab.Application.Abstractions;
using VaultLab.Infrastructure.Documents;
using VaultLab.Infrastructure.Messaging;
using VaultLab.Infrastructure.Persistence;
using VaultLab.Infrastructure.Persistence.Repositories;

if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development")
{
    DotNetEnv.Env.Load("../../.env");
}


var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSingleton<RabbitMqConnection>();
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.Configure<RabbitMqQueueOptions>(builder.Configuration.GetSection("RabbitMqQueues"));
builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));

builder.Services.AddDbContext<VaultLabDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("VaultLab"),
        npgsqlOptions =>
        {
            npgsqlOptions.UseVector();
        }
    )
);

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentTextExtractor, PlainTextDocumentTextExtractor>();
builder.Services.AddScoped<IFileStorage, LocalFIleStorage>();
builder.Services.AddSingleton<IDocumentChunker, SimpleDocumentChunker>();


builder.Services.AddSingleton<EmbeddingClient>(serviceProvider =>
{
    var options = serviceProvider
     .GetRequiredService<Microsoft.Extensions.Options.IOptions<OpenAIOptions>>().Value;

    return new EmbeddingClient(options.EmbeddingModel, options.ApiKey);
});

builder.Services.AddSingleton<IEmbeddingGenerator, OpenAIEmbeddingGenerator>();

builder.Services.AddSingleton<DocumentProcessingConsumer>();
builder.Services.AddHostedService<DocumentProcessingWorker>();


var host = builder.Build();

host.Run();