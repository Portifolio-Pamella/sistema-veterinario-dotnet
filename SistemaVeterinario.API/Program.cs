using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Repositories;
using SistemaVeterinario.API.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));
// Injeção de Dependência
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ITutorRepository, TutorRepository>();
builder.Services.AddScoped<IClinicaRepository, ClinicaRepository>();
builder.Services.AddScoped<IVeterinarioRepository, VeterinarioRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use isto para desabilitar o redirecionamento forçado para HTTPS durante o desenvolvimento, 
// o que costuma causar problemas em ambiente local sem certificado:
// app.UseHttpsRedirection(); 

app.UseAuthorization();
app.MapControllers();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Veterinario API v1");
    // Se você quer que o Swagger abra na raiz (http://localhost:5262/), descomente a linha abaixo:
    c.RoutePrefix = string.Empty;
});

app.Run();