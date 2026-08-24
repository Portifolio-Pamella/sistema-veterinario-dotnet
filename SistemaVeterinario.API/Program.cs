using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.EntityFrameworkCore;

// Os endereços corretos da sua Clean Architecture:
using SistemaVeterinario.Infrastructure;
using SistemaVeterinario.Infrastructure.Repositories;
using SistemaVeterinario.Infrastructure.Repositories.Interfaces;
using SistemaVeterinario.Application.Service;
using SistemaVeterinario.Application.Service.Interface;

var builder = WebApplication.CreateBuilder(args);
// ... o resto do arquivo continua normal