using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Repositories; // Namespace onde reside PetClinicaRepository
using SistemaVeterinario.API.Repositories.Interfaces;
using SistemaVeterinario.API.Services;     // Namespace onde residem os Services

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// 2. Injeção de Dependência dos Repositories
builder.Services.AddScoped<IAcompanhamentoRepository, AcompanhamentoRepository>();
builder.Services.AddScoped<IClinicaRepository, ClinicaRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IFichaClinicaRepository, FichaClinicaRepository>();
builder.Services.AddScoped<IHistoricoRepository, HistoricoRepository>();
builder.Services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
builder.Services.AddScoped<INotificacaoRepository, NotificacaoRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetClinicaRepository, PetClinicaRepository>(); // Agora ele encontra!
builder.Services.AddScoped<ITutorRepository, TutorRepository>();
builder.Services.AddScoped<IVeterinarioRepository, VeterinarioRepository>();

// 3. Injeção de Dependência dos Services
builder.Services.AddScoped<IAcompanhamentoService, AcompanhamentoService>();
builder.Services.AddScoped<IClinicaService, ClinicaService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
builder.Services.AddScoped<IFichaClinicaService, FichaClinicaService>();
builder.Services.AddScoped<IHistoricoService, HistoricoService>();
builder.Services.AddScoped<IMedicamentoService, MedicamentoService>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IPetClinicaService, PetClinicaService>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<IVeterinarioService, VeterinarioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Veterinário API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();