using VaultLab.Application;
using VaultLab.Infrastructure;

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    DotNetEnv.Env.Load("../../.env");
}

var builder = WebApplication.CreateBuilder(args);

// Application & Infrastructure
builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddFileStorage()
    .AddOpenAI(builder.Configuration)
    .AddRabbitMq(builder.Configuration);

// API
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Startup tasks
await app.Services.SeedDatabaseAsync();
await app.Services.InitializeRabbitMqAsync();

// HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
