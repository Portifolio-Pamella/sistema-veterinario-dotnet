using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.EntityFrameworkCore;

// Namespaces das camadas da sua Clean Architecture
using SistemaVeterinario.Infrastructure;
using SistemaVeterinario.Application.Service;

var builder = WebApplication.CreateBuilder(args);

// 1. Adicionar os controllers da API
builder.Services.AddControllers();

// 2. Configurar o Swagger/OpenAPI para documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Configurar o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Veterinario API v1");
        c.RoutePrefix = string.Empty; // Faz o Swagger abrir diretamente na raiz (http://localhost:5262/)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();