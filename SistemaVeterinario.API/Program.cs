using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext integrado com o Driver Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Registro dos Repositórios para Injeção de Dependência
builder.Services.AddScoped<IPetRepository, PetRepository>();

builder.Services.AddControllers();

// Ativa e Configura o Open API / Swagger para documentação profissional
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura a Interface do Swagger disponível em ambiente de Desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Veterinário API v1");
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();