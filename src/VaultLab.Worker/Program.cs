using VaultLab.Infrastructure;
using VaultLab.Worker.Messaging;

if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development")
{
    DotNetEnv.Env.Load("../../.env");
}

var builder = Host.CreateApplicationBuilder(args);

// Infrastructure
builder.Services
    .AddPersistence(builder.Configuration)
    .AddFileStorage()
    .AddOpenAI(builder.Configuration)
    .AddRabbitMq(builder.Configuration)
    .AddDocumentProcessing();

// Worker
builder.Services.AddSingleton<DocumentProcessingConsumer>();
builder.Services.AddHostedService<DocumentProcessingWorker>();

var host = builder.Build();

host.Run();
