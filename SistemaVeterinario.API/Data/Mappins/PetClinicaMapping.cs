using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Data.Mappings
{
    public class PetClinicaMapping : IEntityTypeConfiguration<PetClinica>
    {
        public void Configure(EntityTypeBuilder<PetClinica> builder)
        {
            builder.ToTable("TB_PET_CLINICA");

            builder.HasKey(pc => pc.IdPetClinica);

            builder.Property(pc => pc.IdPetClinica).HasColumnName("ID_PET_CLINICA");

            builder.Property(pc => pc.IdClinica).HasColumnName("ID_CLINICA").IsRequired();

            builder.Property(pc => pc.IdPet).HasColumnName("ID_PET").IsRequired();

            builder.Property(pc => pc.DataVinculoPetClinica)
                .HasColumnName("DATA_VINCULO_PET_CLINICA")
                .IsRequired();

            // Configuração dos Relacionamentos
            builder.HasOne(pc => pc.Clinica)
                .WithMany()
                .HasForeignKey(pc => pc.IdClinica)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pc => pc.Pet)
                .WithMany()
                .HasForeignKey(pc => pc.IdPet)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}