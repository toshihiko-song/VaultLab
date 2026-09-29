using Microsoft.EntityFrameworkCore;
using VaultLab.Infrastructure.Storage;
using VaultLab.Application.Abstractions;
using VaultLab.Infrastructure.Persistence;
using VaultLab.Infrastructure.Persistence.Repositories;
using VaultLab.Application.Features.Documents.Commands.UploadDocument;
using VaultLab.Infrastructure.Messaging;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(
    cfg =>
        cfg.RegisterServicesFromAssembly(
            typeof(UploadDocumentCommand).Assembly
        )
);

builder.Services.AddDbContext<VaultLabDbContext>(options => 
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("VaultLab")
    )
);

builder.Services.AddScoped<IFileStorage, LocalFIleStorage>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IMessagePublisher, RabbitMqMessagePublisher>();


builder.Services.Configure<RabbitMqOptions>(
    builder.Configuration.GetSection("RabbitMQ"));


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VaultLabDbContext>();

    await DbInitializer.SeedAsync(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
