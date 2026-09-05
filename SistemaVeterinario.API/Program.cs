using SistemaVeterinario.Infrastructure.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// 1. Injeta as dependências da infraestrutura (inclui o banco e o readiness check)
builder.Services.AddInfrastructure(builder.Configuration);

// 2. Adiciona o Liveness check diretamente na API
builder.Services.AddHealthChecks()
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy(), tags: new[] { "live" });

var app = builder.Build();

// 3. Mapeia o Endpoint de Liveness (Vivacidade)
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live")
});

// 4. Mapeia o Endpoint de Prontidão (Readiness) com retorno em JSON
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.Run();