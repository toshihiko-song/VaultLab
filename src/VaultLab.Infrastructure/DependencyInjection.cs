using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenAI.Chat;
using OpenAI.Embeddings;
using VaultLab.Application.Abstractions;
using VaultLab.Infrastructure.AI;
using VaultLab.Infrastructure.Authentication;
using VaultLab.Infrastructure.Configuration;
using VaultLab.Infrastructure.Documents;
using VaultLab.Infrastructure.Messaging;
using VaultLab.Infrastructure.Persistence;
using VaultLab.Infrastructure.Persistence.Repositories;
using VaultLab.Infrastructure.Storage;

namespace VaultLab.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {

            var secretPrefix = environment.IsDevelopment()
                ? "vaultlab/dev"
                : "vaultlab/prod";

            var databaseSettings = configuration
                .GetSection($"{secretPrefix}/database")
                .Get<DatabaseSettings>()
                ?? throw new InvalidOperationException("Database settings are missing from configuration");

            var connectionString = DatabaseConnectionStringFactory.Create(databaseSettings);

            services.AddDbContext<VaultLabDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.UseVector()
                )
            );

            services.AddScoped<IDocumentRepository, DocumentRepository>();

            return services;
        }

        public static IServiceCollection AddFileStorage(this IServiceCollection services)
        {
            services.AddScoped<IFileStorage, LocalFIleStorage>();

            return services;
        }

        public static IServiceCollection AddAIAgent(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {

            var secretPrefix = environment.IsDevelopment()
                ? "vaultlab/dev"
                : "vaultlab/prod";

            var openAiSettings = configuration.GetSection($"{secretPrefix}/openai") ??
                throw new InvalidOperationException("Open AI configuration missing.");


            services.Configure<OpenAISettings>(openAiSettings);

            services.AddSingleton(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<OpenAISettings>>().Value;

                return new EmbeddingClient(options.EmbeddingModel, options.ApiKey);
            });

            services.AddSingleton(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<OpenAISettings>>().Value;

                return new ChatClient(options.ChatModel, options.ApiKey);
            });

            services.AddSingleton<IEmbeddingGenerator, OpenAIEmbeddingGenerator>();
            services.AddSingleton<ILanguageModel, OpenAILanguageModel>();

            return services;
        }

        public static IServiceCollection AddMessagingQueue(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            var secretPrefix = environment.IsDevelopment()
                ? "vaultlab/dev"
                : "vaultlab/prod";

            var rabbitMqSettings = configuration.GetSection($"{secretPrefix}/rabbitmq");

            services.Configure<RabbitMqSettings>(rabbitMqSettings);

            //TODO use constant for these
            services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));

            services.AddSingleton<RabbitMqConnection>();
            services.AddSingleton<RabbitMqInitializer>();
            services.AddScoped<IMessagePublisher, RabbitMqMessagePublisher>();

            return services;
        }

        public static IServiceCollection AddDocumentProcessing(this IServiceCollection services)
        {
            services.AddScoped<IDocumentTextExtractor, PlainTextDocumentTextExtractor>();
            services.AddScoped<IDocumentTextExtractor, PdfDocumentTextExtractor>();
            services.AddScoped<IDocumentTextExtractor, MarkdownDocumentExtractor>();
            services.AddScoped<IDocumentTextExtractor, DocxDocumentTextExtractor>();
            services.AddScoped<IDocumentTextExtractor, ExcelDocumentTextExtractor>();
            services.AddScoped<IDocumentTextExtractorFactory, DocumentTextExtractorFactory>();

            services.AddSingleton<IDocumentChunker, SimpleDocumentChunker>();

            return services;
        }


        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            var secretPrefix = environment.IsDevelopment()
                ? "vaultlab/dev"
                : "vaultlab/prod";

            var authOptions = configuration.GetSection($"{secretPrefix}/googleAuth");


            services.Configure<GoogleAuthenticationOptions>(authOptions);

            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection($"{secretPrefix}/jwt"))
                .Validate(options =>
                    !string.IsNullOrWhiteSpace(options.Issuer) &&
                    !string.IsNullOrWhiteSpace(options.Audience) &&
                    !string.IsNullOrWhiteSpace(options.SecretKey) &&
                    options.AccessTokenExpirationMinutes > 0,
                    "JWT configuration is invalid"
                )
                .ValidateOnStart();


            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    options =>
                    {
                        var jwtOptions = configuration
                            .GetSection($"{secretPrefix}/jwt")
                            .Get<JwtOptions>()!;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = jwtOptions.Issuer,

                            ValidateAudience = true,
                            ValidAudience = jwtOptions.Audience,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtOptions.SecretKey)
                            ),

                            ValidateLifetime = true,

                            ClockSkew = TimeSpan.Zero
                        };
                    }
                );

            services.AddAuthorization();

            services.AddScoped<IExternalAuthenticationService, GoogleExternalAuthenticationService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccessTokenService, JwtAccessTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            return services;

        }
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<VaultLabDbContext>();

            await DbInitializer.SeedAsync(context);
        }

        public static async Task InitializeRabbitMqAsync(this IServiceProvider serviceProvider)
        {
            var rabbitMqInitializer = serviceProvider.GetRequiredService<RabbitMqInitializer>();

            await rabbitMqInitializer.InitializeAsync();
        }
    }
}
