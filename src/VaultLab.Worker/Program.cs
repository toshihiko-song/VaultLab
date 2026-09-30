using VaultLab.Infrastructure.Messaging;
using VaultLab.Worker.Messaging;

if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development")
{
    DotNetEnv.Env.Load("../../.env");
}


var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSingleton<RabbitMqConnection>();
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.Configure<RabbitMqQueueOptions>(builder.Configuration.GetSection("RabbitMqQueues"));

builder.Services.AddSingleton<DocumentProcessingConsumer>();

builder.Services.AddHostedService<DocumentProcessingWorker>();


var host = builder.Build();

host.Run();