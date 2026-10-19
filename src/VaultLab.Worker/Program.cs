using VaultLab.Infrastructure;
using VaultLab.Worker.Messaging;
using VaultLab.Infrastructure.Configuration;


var builder = Host.CreateApplicationBuilder(args);

//Get Secrets
if (!builder.Environment.IsEnvironment("Testing"))
{
    var environment = builder.Environment.IsDevelopment()
        ? "dev"
        : "prod";

    await builder.Configuration.AddSecretsManagerAsync(
        "ap-southeast-1",
        [
            $"vaultlab/{environment}/database",
            $"vaultlab/{environment}/rabbitmq",
            $"vaultlab/{environment}/openai"
        ]);
}

// Infrastructure
builder.Services
    .AddPersistence(builder.Configuration, builder.Environment)
    .AddFileStorage()
    .AddAIAgent(builder.Configuration, builder.Environment)
    .AddMessagingQueue(builder.Configuration, builder.Environment)
    .AddDocumentProcessing();

// Worker
builder.Services.AddSingleton<DocumentProcessingConsumer>();
builder.Services.AddHostedService<DocumentProcessingWorker>();

var host = builder.Build();

host.Run();
