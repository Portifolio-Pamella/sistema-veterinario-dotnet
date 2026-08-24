using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.Infrastructure.Data;

namespace SistemaVeterinario.API.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Configuração do DbContext usando Oracle
        services.AddDbContext<AppDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString("OracleConnection")));

        // 2. Configuração dos Health Checks integrados com o Banco de Dados
        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>(
                name: "database-check",
                tags: new[] { "ready" });

        return services;
    }
}