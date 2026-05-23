using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veterinario>(entity =>
            {
                entity.ToTable("TB_VETERINARIO");
                entity.HasKey(v => v.IdVeterinario);
                entity.Property(v => v.IdVeterinario)
                      .HasColumnName("ID_VETERINARIO")
                      .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.ToTable("TB_TUTOR");
                entity.HasKey(t => t.IdTutor);
                entity.Property(t => t.IdTutor)
                      .HasColumnName("ID_TUTOR")
                      .ValueGeneratedOnAdd();

                entity.HasIndex(t => t.CpfTutor).IsUnique();
                entity.HasIndex(t => t.EmailTutor).IsUnique();
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("TB_PET");
                entity.HasKey(p => p.IdPet);
                entity.Property(p => p.IdPet)
                      .HasColumnName("ID_PET")
                      .ValueGeneratedOnAdd();
            });
        }
    }
}