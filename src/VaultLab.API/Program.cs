using VaultLab.API.Exceptions;
using VaultLab.Application;
using VaultLab.Infrastructure;
using VaultLab.Infrastructure.Configuration;





var builder = WebApplication.CreateBuilder(args);

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
            $"vaultlab/{environment}/openai",
            $"vaultlab/{environment}/googleAuth",
            $"vaultlab/{environment}/jwt",
        ]);
}

// Application & Infrastructure
builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration, builder.Environment)
    .AddFileStorage()
    .AddAIAgent(builder.Configuration, builder.Environment)
    .AddAuthentication(builder.Configuration, builder.Environment)
    .AddMessagingQueue(builder.Configuration, builder.Environment);


// API
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(options => { });

// Startup tasks
if (!app.Environment.IsEnvironment("Testing"))
{
    await app.Services.SeedDatabaseAsync();
    await app.Services.InitializeRabbitMqAsync();
}


// HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }
