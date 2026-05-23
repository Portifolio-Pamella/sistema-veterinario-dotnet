using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Repositories;
using SistemaVeterinario.API.Repositories.Interfaces;
using SistemaVeterinario.API.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// 2. Configuração de CORS (DEVE ser antes do builder.Build())
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 3. Injeção de Dependência (Repositories e Services)
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ITutorRepository, TutorRepository>();
builder.Services.AddScoped<IVeterinarioRepository, VeterinarioRepository>();

builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<IVeterinarioService, VeterinarioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AQUI o build congela a coleção de serviços
var app = builder.Build();

// 4. Middlewares (Devem vir após o builder.Build())
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Veterinário API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Ativa a política de CORS definida acima
app.UseCors("AllowAll");

app.MapControllers();

app.Run();