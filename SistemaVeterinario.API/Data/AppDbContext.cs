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
        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Acompanhamento> Acompanhamentos { get; set; }
        public DbSet<FichaClinica> FichasClinicas { get; set; }
        public DbSet<Historico> Historicos { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<PetClinica> PetClinicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Garante as regras de unicidade mapeadas na DDL (Unique Constraints)
            modelBuilder.Entity<Clinica>()
                .HasIndex(c => c.CnpjClinica).IsUnique();

            modelBuilder.Entity<Clinica>()
                .HasIndex(c => c.EmailClinica).IsUnique();

            modelBuilder.Entity<Tutor>()
                .HasIndex(t => t.CpfTutor).IsUnique();

            modelBuilder.Entity<Tutor>()
                .HasIndex(t => t.EmailTutor).IsUnique();

            modelBuilder.Entity<Veterinario>()
                .HasIndex(v => v.CrmVeterinario).IsUnique();

            modelBuilder.Entity<Veterinario>()
                .HasIndex(v => v.EmailVeterinario).IsUnique();

            modelBuilder.Entity<FichaClinica>()
                .HasIndex(f => f.IdPet).IsUnique();
        }
    }
}