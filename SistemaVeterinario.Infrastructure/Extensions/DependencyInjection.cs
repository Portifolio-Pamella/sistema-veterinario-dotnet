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
            services.AddDbContext<AppDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString("OracleConnection")));
            services.AddScoped<ITutorRepository, TutorRepository>();
            services.AddScoped<IVeterinarioRepository, VeterinarioRepository>();
            services.AddScoped<IPetRepository, PetRepository>();

            return services;
        }
    }
}