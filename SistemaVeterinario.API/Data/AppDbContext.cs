using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Caso queira configurar chaves compostas ou sequences manuais do Oracle, adicione aqui.
            base.OnModelCreating(modelBuilder);
        }
    }
}