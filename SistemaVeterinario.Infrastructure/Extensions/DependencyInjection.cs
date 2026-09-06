using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.Infrastructure.Data;
using SistemaVeterinario.Infrastructure.Repositories;
using SistemaVeterinario.Infrastructure.Repositories.Interfaces;

namespace SistemaVeterinario.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OracleConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseOracle(connectionString));

            // Adiciona o Health Check apontando para o DbContext do Oracle com a tag "ready"
            services.AddHealthChecks()
                .AddDbContextCheck<AppDbContext>(
                    name: "database-check",
                    tags: new[] { "ready" });

            services.AddScoped<IVeterinarioRepository, VeterinarioRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<ITutorRepository, TutorRepository>();

            return services;
        }
    }
}