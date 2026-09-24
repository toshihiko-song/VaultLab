using MediatR;
using VaultLab.Infrastructure.Storage;
using VaultLab.Application.Abstractions;
using VaultLab.Application.Features.Documents.Commands.UploadDocument;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IFileStorage, LocalFIleStorage>();

builder.Services.AddMediatR(
    cfg =>
        cfg.RegisterServicesFromAssembly(
            typeof(UploadDocumentCommand).Assembly
        )
);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
